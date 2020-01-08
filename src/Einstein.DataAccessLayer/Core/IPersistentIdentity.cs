using System.Security.Principal;

namespace Einstein.DataAccessLayer.Core
{
  public interface IPersistentIdentity : IIdentity
	{
		IPersistentUser PersistentUser { get; }
	}
}
