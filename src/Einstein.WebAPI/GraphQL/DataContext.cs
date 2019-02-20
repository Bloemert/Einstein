using Bloemert.Data.Entity.Auth.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einstein.WebAPI.GraphQL
{
	public class DataContext
	{
		public IEnumerable<User> Users { get; set; }
	}
}
