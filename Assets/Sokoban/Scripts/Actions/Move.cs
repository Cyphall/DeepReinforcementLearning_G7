using Common.Core;
using UnityEngine;

namespace Sokoban
{
	public abstract class Move : AGameAction<GameState>
	{
		protected GameState ApplyMovement(GameState state, Vector2Int direction)
		{
			state.CurrentIteration += 1;

			Vector2Int nextPlayerPos = state.AgentPos + direction;
			
			if (state.Grid[nextPlayerPos.x, nextPlayerPos.y] == TileType.Wall)
			{
				return state;
			}

			Vector2Int testedPos = state.AgentPos;
			bool doable = false;
			while (true)
			{
				testedPos += direction;
				if (state.Grid[testedPos.x, testedPos.y] == TileType.Wall)
				{
					break;
				}

				if (!state.AnyEntity(testedPos.x, testedPos.y))
				{
					doable = true;
					break;
				}
			}

			if (!doable)
				return state;
			
			for (Vector2Int pos = testedPos; pos != state.AgentPos; pos -= direction)
			{
				for (int i = 0; i < state.CratePos.Length; ++i)
				{
					if (state.CratePos[i] == pos)
					{
						state.CratePos[i] += direction;
					}
				}
			}

			state.AgentPos = nextPlayerPos;

			return state;
		}
	}
}