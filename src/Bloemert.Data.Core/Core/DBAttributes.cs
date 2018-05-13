using System;

namespace Bloemert.Data.Core
{

		[AttributeUsage(AttributeTargets.Property)]
		public class DbColumnAttribute : System.Attribute
		{
			public bool PrimaryKey { get; set; }
		}

		[AttributeUsage(AttributeTargets.Property)]
		public class DbBLazyLoadColumnAttribute : System.Attribute
		{
			public string ColumnId { get; set; }
		}


		[AttributeUsage(AttributeTargets.Class)]
		public class DbTableAttribute : System.Attribute
		{
			public string TableName { get; set; }
		}

	}
