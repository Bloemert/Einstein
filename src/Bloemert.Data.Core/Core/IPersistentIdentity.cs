using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Bloemert.Data.Core.Core
{
	public interface IPersistentIdentity : IIdentity
	{
		IPersistentUser PersistentUser { get; }
	}
}
