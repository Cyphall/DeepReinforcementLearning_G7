using Common.Core;
using UnityEngine;

namespace Sokoban.Agent
{
	public class PlayerAgent : AAgent<GameState>
	{
		public PlayerAgent() : base()
		{
			
		}

		public override AGameAction<GameState> GetAction(GameState state)
		{
			if (Input.GetKeyDown(KeyCode.Z))
			{
				return new MoveUp();
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				return new MoveLeft();
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				return new MoveDown();
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				return new MoveRight();
			}

			return null;
		}
	}
}