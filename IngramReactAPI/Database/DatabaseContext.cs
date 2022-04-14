using IngramReactAPI.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngramReactAPI.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-7JOA7RT;Initial Catalog=BudgetDataBase;Integrated Security=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
