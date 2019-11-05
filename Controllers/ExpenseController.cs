using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseApi.Models;

namespace ExpenseApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ExpenseController : ControllerBase
  {
    private readonly ExpenseApiDatabaseContext _context;

    public ExpenseController(ExpenseApiDatabaseContext context)
    {
      _context = context;
    }

    // GET: api/Expense
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
    {
      return await _context.Expenses.ToListAsync();
    }

    // GET: api/Expense/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Expense>> GetExpense(int id)
    {
      var expense = await _context.Expenses.FindAsync(id);

      if (expense == null)
      {
        return NotFound();
      }

      return expense;
    }

    // PUT: api/Expense/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutExpense(int id, Expense expense)
    {
      if (id != expense.Id)
      {
        return BadRequest();
      }

      _context.Entry(expense).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ExpenseExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Expense
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    public async Task<ActionResult<Expense>> PostExpense(Expense expense)
    {
      expense.When = DateTime.UtcNow;
      _context.Expenses.Add(expense);
      await _context.SaveChangesAsync();

      return Ok(expense);
    }

    // DELETE: api/Expense/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Expense>> DeleteExpense(int id)
    {
      var expense = await _context.Expenses.FindAsync(id);
      if (expense == null)
      {
        return NotFound();
      }

      _context.Expenses.Remove(expense);
      await _context.SaveChangesAsync();

      return expense;
    }

    private bool ExpenseExists(int id)
    {
      return _context.Expenses.Any(e => e.Id == id);
    }
  }
}
