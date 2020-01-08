using Bloemert.Data.Entity.Auth.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.Data.Core.Core
{
  public class EinsteinDbContext : DbContext
  {

	public EinsteinDbContext(DbContextOptions<EinsteinDbContext> options) : base(options)
	{
	}

	public DbSet<User> Users { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
	  // Configure database attributes
	  modelBuilder.ApplyConfiguration<BaseMap<User>>
	}



  }
}
