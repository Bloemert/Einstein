using Bloemert.Data.Entity.Admin.Entity;
using Bloemert.Lib.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Bloemert.WebAPI.Admin.Models
{

	public class LogModel : IModel
	{
		public int Id { get; set; }

		public DateTime EffectiveStartedOn { get; set; }
		public int EffectiveStartedBy { get; set; }

		public DateTime EffectiveModifiedOn { get; set; }
		public int EffectiveModifiedBy { get; set; }

		public DateTime EffectiveEndedOn { get; set; }
		public int EffectiveEndedBy { get; set; }



		public string Message { get; set; }
		public string Level { get; set; }
		public DateTimeOffset  TimeStamp { get; set; }


		public string Exception { get; set; }

		public XmlDocument Properties {get;set;}

		public string LogEvent {get;set;}
	}
}
