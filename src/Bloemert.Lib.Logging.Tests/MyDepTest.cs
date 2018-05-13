using Serilog;

namespace Bloemert.Lib.Logging.Tests
{
	public class MyDepTest : IMyDepTest
	{
		public MyDepTest(ILogger Log)
		{
			Log.Debug("Test maar eens {Name} of dit werkt!", "Henry");
		}
	}
}
