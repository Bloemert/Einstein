using Einstein.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Data.SqlTypes;

namespace Bloemert.Data.Entity.Auth.Mappings
{
  public class UserMap : IEntityTypeConfiguration<User>
	{
	 public void Configure(EntityTypeBuilder<User> builder)
	 {
		builder
			.ToTable("Users");

		builder
			.HasKey(k => k.Id);

		var adminId = Guid.NewGuid();
		builder
			.HasData(
				new User
				{
				  Id = adminId,
				  Active = true,
				  UserName = "admin",
				  PasswordData = "8a8ab4ae3b063c19103ddf08ab7fda9ec118abc73fbb65c1cd12217adbdaed1d52e5db1337cbf0f275641494823c36e631ccc8ac353459177bd41718df0b56f6",
				  Email = "einstein@bloemert.com",
				  Firstname = "Administrator",
				  Lastname = "Bloemert",
				  EffectiveStartedOn = DateTime.Now,
				  EffectiveStartedBy = adminId,
				  EffectiveEndedOn = SqlDateTime.MaxValue.Value
				});
	 }
  }
}
