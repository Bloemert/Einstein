using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloemert.Data.Core.Core
{
  public class BaseMap<T> : IEntityTypeConfiguration<T>
		where T : BaseEntity
  {

	public void Configure(EntityTypeBuilder<T> builder)
	{
	  // Add special configuration mappings between DB and DataLayer hier.
	  builder.HasKey(k => k.Id);

	}
  }

}
