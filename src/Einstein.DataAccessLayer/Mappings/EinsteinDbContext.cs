using Einstein.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Einstein.DataAccessLayer.Mappings
{
  public class EinsteinDbContext : DbContext
  {

	 public EinsteinDbContext(DbContextOptions<EinsteinDbContext> options) : base(options)
	 {
	 }

	 
	 public DbSet<Employee> Employees { get; set; }

	 public DbSet<User> Users { get; set; }


	 protected override void OnModelCreating(ModelBuilder modelBuilder)
	 {
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	 }



  }
}
