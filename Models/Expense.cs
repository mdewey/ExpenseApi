using System;

namespace ExpenseApi.Models
{
  public class Expense
  {
    public int Id { get; set; }
    public string Note { get; set; }
    public string Type { get; set; }
    public DateTime? When { get; set; }
    public double Amount { get; set; }
  }
}