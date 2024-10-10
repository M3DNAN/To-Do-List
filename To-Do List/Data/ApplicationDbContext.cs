using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;
using To_Do_List.Models;
using System.Reflection.Emit;

namespace To_Do_List.Data
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var Builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            var connection = Builder.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connection);
        }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("IdNumbers");
            modelBuilder.HasSequence<int>("Id");

            modelBuilder.Entity<Customer>()
                .Property(o => o.Id)
                .HasDefaultValueSql("NEXT VALUE FOR IdNumbers");
            modelBuilder.Entity<ToDoItem>()
                .Property(o => o.Id)
                .HasDefaultValueSql("NEXT VALUE FOR Id");
        }  
    }
}


