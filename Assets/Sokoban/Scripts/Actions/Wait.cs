using Common.Core;

namespace Sokoban
{
	public class Wait : AGameAction<GameState>
	{
		public override GameState Apply(GameState state)
		{
			return state;
		}
	}
}