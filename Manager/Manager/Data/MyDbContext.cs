using Manager.Models;
using Microsoft.EntityFrameworkCore;

namespace Manager.Data
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext>options):base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Branch> Branches { get; set; }
    }
}
