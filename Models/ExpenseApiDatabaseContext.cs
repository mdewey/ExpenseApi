using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExpenseApi.Models
{
  public partial class ExpenseApiDatabaseContext : DbContext
  {
    public ExpenseApiDatabaseContext()
    {
    }

    public ExpenseApiDatabaseContext(DbContextOptions<ExpenseApiDatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<Expense> Expenses { get; set; }


    private string ConvertPostConnectionToConnectionString(string connection)
    {
      var _connection = connection.Replace("postgres://", String.Empty);
      var output = Regex.Split(_connection, ":|@|/");
      return $"server={output[2]};database={output[4]};User Id={output[0]}; password={output[1]}; port={output[3]}";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        var envConn = Environment.GetEnvironmentVariable("DATABASE_URL");
        var conn = "Server=localhost;Database=ExpenseApiDatabase";
        if (envConn != null)
        {
          conn = ConvertPostConnectionToConnectionString(envConn);
        }
        optionsBuilder.UseNpgsql(conn);
      }
    }
  }
}
