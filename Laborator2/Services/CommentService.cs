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

    public class CommentService : ICommentService
    {
        private ExpensesDbContext context;

        public CommentService(ExpensesDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<CommentsGetModel> GetAllComments(string filterText)
        {
            IQueryable<Expense> result = context.Expenses.Include(c => c.Comments);

            List<CommentsGetModel> resultComments = new List<CommentsGetModel>();
            List<CommentsGetModel> resultCommentsNoFilter = new List<CommentsGetModel>();

            foreach (Expense expense in result)
            {
                expense.Comments.ForEach(comment =>
                {
                    CommentsGetModel newComment = CommentsGetModel.ConvertToCommentsGetModel(comment, expense);
                    
                    if (comment.Text == null || filterText == null)
                    {
                        resultCommentsNoFilter.Add(newComment);
                    }
                    else if (comment.Text.Contains(filterText))
                    {
                        resultComments.Add(newComment);
                    }
                });
            }

            //dysplay result 
            if (filterText == null)
            {
                return resultCommentsNoFilter;
            }
                return resultComments;
        }

    }
}
