using kersenduz.Shared.Models;
using Microsoft.EntityFrameworkCore;


namespace kersenduz.DataAccess.Context;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions options) : base(options)
  {

  }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {

  }
  public DbSet<User> Users { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<RawMaterial> RawMaterials { get; set; }
  public DbSet<ProductRawMaterial> ProductRawMaterials{ get; set; }
  public DbSet<Category> Categories { get; set; }
  public DbSet<Unit> Units { get; set; }
}
