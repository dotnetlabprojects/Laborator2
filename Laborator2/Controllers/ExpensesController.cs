using Laborator2.Models;
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
        private ExpensesDbContext context;

        public ExpensesController(ExpensesDbContext context)
        {
            this.context = context;
        }
        // GET: api/Expenses
        [HttpGet]
        public IEnumerable<Expense> Get([FromQuery]Type? type, [FromQuery]DateTime? from, [FromQuery]DateTime? to)
        {
            IQueryable<Expense> result = context.Expenses.Include(c => c.Comments);

            if (from == null && to == null && type == null)
            {
                return result;
            }
            if (type != null)
            {
                result = result.Where(e => e.Type.Equals(type));
            }

            if (from != null)
            {
                result = result.Where(e => e.Date >= from);
            }
            if (to != null)
            {
                result = result.Where(e => e.Date <= to);
            }
            return result;
        }

        // GET: api/Expenses/1
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var existing = context.Expenses.FirstOrDefault(product => product.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            return Ok(existing);
        }

        // POST: api/Expenses
        [HttpPost]
        public void Post([FromBody] Expense expense)
        {
            context.Expenses.Add(expense);
            context.SaveChanges();
        }

        // PUT: api/Expenses/3
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Expense expense)
        {
            var existing = context.Expenses.AsNoTracking().FirstOrDefault(f => f.Id == id);
            if (existing == null)
            {
                context.Expenses.Add(expense);
                context.SaveChanges();
                return Ok(expense);
            }
            expense.Id = id;
            context.Expenses.Update(expense);
            context.SaveChanges();
            return Ok(expense);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = context.Expenses.Include(e=>e.Comments).FirstOrDefault(product => product.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            context.Expenses.Remove(existing);
            context.SaveChanges();
            return Ok();
        }
    }
}