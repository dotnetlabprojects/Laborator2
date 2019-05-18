﻿using Laborator2.Models;
using Laborator2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private IExpenseService expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            this.expenseService = expenseService;
        }
       
        /// <summary>
        /// Get all expenses
        /// </summary>
       
        /// <param name="type">Optional , filter by type of expense</param>
        /// <param name="from">Optional , filter by minimum date </param>
        /// <param name="to">Optional , filter by maximum date</param>
        /// <returns>List of expenses objects</returns>
        [HttpGet]
        public IEnumerable<Expense> Get([FromQuery]Type? type, [FromQuery]DateTime? from, [FromQuery]DateTime? to)
        {
            return expenseService.GetAll(type,from,to);
        }

        // GET: api/Expenses/1
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var found = expenseService.GetById(id);

            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }

        /// <summary>
        /// Add expense
        /// </summary>
        ///   <remarks>
        /// Sample request:
        ///
        ///  {
        ///  id: 7,
        ///  description: "red dress",
        ///  sum: 500,
        ///  location: "Iulius Mall",
        ///  date: "2011-04-22T00:00:00",
        ///  currency: "lei",
        ///  type: 3,
        ///  comments: [
        ///         {
        ///         id: 3,
        ///         text: "first comment",
        ///         importan: false
        ///         },
        ///         {
        ///         id: 4,
        ///         text: "second comment",
        ///         importan: false
        ///         }
        ///           ]
        ///  }
        ///
        /// </remarks>
        /// <param name="expense">The expense to add</param>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public void Post([FromBody] Expense expense)
        {
            expenseService.Create(expense);
        }

        // PUT: api/Expenses/3
        /// <summary>
        /// Update expense
        /// </summary>
        /// <param name="id"></param>
        /// <param name="expense"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Expense expense)
        {
            var result = expenseService.Upsert(id, expense);
           
                return Ok(expense);
         
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = expenseService.Delete(id);
            if (result == null)
            {
                return NotFound();
            }
           
            return Ok(result);
        }
    }
}