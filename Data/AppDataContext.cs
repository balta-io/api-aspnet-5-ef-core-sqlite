using MeuTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace MeuTodo.Data
{
    public class AppDataContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");
    }
}