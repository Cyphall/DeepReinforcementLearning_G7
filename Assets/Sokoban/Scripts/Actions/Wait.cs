using Common.Core;
using UnityEngine;

namespace Sokoban
{
	public class Wait : Move
	{
		public override GameState Apply(GameState state)
		{
			return state;
		}
	}
}