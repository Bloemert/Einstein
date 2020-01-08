using Einstein.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bloemert.Data.Entity.Auth.Mappings
{
  public class EmployeeMap : IEntityTypeConfiguration<Employee>
	{
	 public void Configure(EntityTypeBuilder<Employee> builder)
	 {
		builder
			.ToTable("Employees");

		builder
			.HasKey(k => k.Id);
	 }
  }
}
