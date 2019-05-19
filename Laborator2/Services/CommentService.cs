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
        IEnumerable<Comment> GetAllComments(string text);  
    }

    public class CommentService: ICommentService
    {
        private ExpensesDbContext context;

        public CommentService(ExpensesDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Comment> GetAllComments(String filter)
        {
            IQueryable<Comment> result = context.Comments;

            List<Comment> comments = new List<Comment>();



            foreach (Comment comment in result)
            {
                if (filter == null)
                {
                    return result;
                }

                if (comment.Text != null && filter != null)
                {

                    if (comment.Text.Contains(filter))
                    {

                        comments.Add(comment);
                    }
                }

            }


            return comments;
        }
    }

}

