using kersenduz.Shared.Models;
using Microsoft.EntityFrameworkCore;


namespace kersenduz.DataAccess.Context;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
    public DbSet<User> Users { get; set; }
}
