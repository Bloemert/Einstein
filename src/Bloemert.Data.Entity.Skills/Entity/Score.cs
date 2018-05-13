
namespace Einstein.Data.Entity.Knowledge
{
	public class Score : Item
	{
		public virtual decimal Number { get; set; }

		// Association(s)
		public virtual Topic Topic { get; set; }
	}
}
