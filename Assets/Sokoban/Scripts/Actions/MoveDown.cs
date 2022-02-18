using Common.Core;
using UnityEngine;

namespace Sokoban
{
	public class MoveDown : Move
	{
		public override GameState Apply(GameState state)
		{
			Vector2Int direction = new Vector2Int(0, -1);

			return ApplyMovement(state, direction);
		}
	}
}