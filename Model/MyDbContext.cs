using Microsoft.EntityFrameworkCore;
using Model.Entity;

namespace Model;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) {
    }
    
    public DbSet<User> User { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Menu> Menu { get; set; }
    public DbSet<RoleToMenu> RoleToMenu { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasData(new List<User>()
            {
                new User(){Id = 1, Name = "管理员", LoginName = "admin", Password = "123456"}
            });
    }
}