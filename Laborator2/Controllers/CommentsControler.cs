using Laborator2.Models;
using Laborator2.Services;
using Laborator2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsControler : ControllerBase
    {
        private ICommentService commentService;

        public CommentsControler(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter">Optional , filter by comments text</param>
        /// <returns>A list of comments</returns>
        [HttpGet]
         
        public IEnumerable<Comment> Get([FromQuery] String filter)
        {
            return commentService.GetAllComments(filter);

        }
    }
}
