using Laborator2.Models;
using Laborator2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator2.Services
{
    public interface ICommentService
    {
        IEnumerable<CommentsGetModel> GetAllComments(string text);  
    }

    public class CommentService: ICommentService
    {
        private ExpensesDbContext context;

        public CommentService(ExpensesDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<CommentsGetModel> GetAllComments(string text)
        {
            IQueryable<Expense> result = context.Expenses.Include(c => c.Comments);

            List<CommentsGetModel> resultComments = new List<CommentsGetModel>();
            List<CommentsGetModel> resultCommentsNoFilter = new List<CommentsGetModel>();

            foreach ( Expense e in result)
            {
                e.Comments.ForEach(c =>
                {
                    if(c.Text==null||text==null )
                    {
                        CommentsGetModel comment = new CommentsGetModel
                        {
                            Id = c.Id,
                            Importan = c.Importan,
                            Text = c.Text,
                            expenseId = e.Id

                        };
                        resultCommentsNoFilter.Add(comment);


                    }else if (c.Text.Contains(text))
                    {
                        CommentsGetModel comment = new CommentsGetModel
                        {
                            Id = c.Id,
                            Importan = c.Importan,
                            Text = c.Text,
                            expenseId = e.Id

                        };
                        resultComments.Add(comment);

                    }
                }  );
            }
            if (text == null)
            {
                return resultCommentsNoFilter;
            }
            return resultComments;
        }

    }
}
