using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Bloemert.Data.Core
{
	public class DbColumnInfo
	{
		public string Name { get; set; }

		public bool IsExcludedProperty { get; set; } = false;

		public string ColumnName { get; set; }
		public bool IsIdentity { get; set; }
		public bool IsNullable { get; set; }
		public bool IsRowGuidCol { get; set; }
		public bool IsXmlDocument { get; set; }
		public bool IsComputed { get; set; }
		public string DataType { get; set; }
		public long MaxLength { get; set; }
		public int Precision { get; set; }
		public int Scale { get; set; }
		public string DefaultValue { get; set; }
	}
}
