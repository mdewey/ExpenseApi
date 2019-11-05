dotnet ef dbcontext scaffold "Server=localhost;Database=ExpenseApiDatabase" Npgsql.EntityFrameworkCore.PostgreSQL -o Models

dotnet aspnet-codegenerator controller -name ExpenseController -api -m Expense -dc ExpenseApiDatabaseContext -outDir Controllers
