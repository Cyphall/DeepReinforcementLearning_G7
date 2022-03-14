using Common.Core;
using Common.Enumeration;
using Sokoban.Agent;
using Sokoban.Game;
using UnityEngine;

namespace Sokoban
{
    public class PlayerScript : MonoBehaviour
    {
        private PlayerAgent _agent;
        public GameManager GameManager { get; set; }

        private AGameAction<GameState> _nextAction;

        private void Start()
        {
            this._agent = new PlayerAgent(this.GameManager.GameRules);
        }

        private void Update()
        {
            if (GameManager.GameState.Status != GameStatus.Playing) return;

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
        }
    }
}