using Common.Core;
using Sokoban.Agent;
using System.Collections.Generic;
using UnityEngine;

namespace Sokoban
{
	public class PlayerScript : MonoBehaviour
	{
		private PlayerAgent _agent = new PlayerAgent(new List<AGameAction<GameState>>());
		public GameManager GameManager { get; set; }

		private AGameAction<GameState> _nextAction;

		private void Update()
		{
			AGameAction<GameState> action = _agent.GetAction(GameManager.GameState);
			if (action != null)
			{
				_nextAction = action;
			}
		}

		private void FixedUpdate()
		{
			if (_nextAction != null)
			{
				GameManager.ApplyAction(_nextAction);
				_nextAction = null;
			}
			else
			{
				GameManager.ApplyAction(new Wait());
			}
		}
	}
}