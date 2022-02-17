using Common.Core;

namespace GridWorld
{
	public class Wait : AGameAction<GameState>
	{
		public override GameState Apply(GameState state)
		{
			return state;
		}
	}
}