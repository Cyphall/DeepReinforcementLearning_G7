using Common.Core;
using GridWorld.Game;
using UnityEngine;

namespace GridWorld.Agent
{
    public class PlayerAgent : AAgent<GameState, GameRules>
    {
        #region Constructeur

        public PlayerAgent(GameRules rules) : base(rules) { }

        #endregion

        #region Méthodes publiques 

        public override AGameAction<GameState> GetAction(GameState gameState)
        {
            if (Input.GetKeyDown(KeyCode.Z))
                return this._rules.MoveUp;
            if (Input.GetKeyDown(KeyCode.Q))
                return this._rules.MoveLeft;
            if (Input.GetKeyDown(KeyCode.S))
                return this._rules.MoveDown;
            if (Input.GetKeyDown(KeyCode.D))
                return this._rules.MoveRight;

            return null;
        }

        #endregion
    }
}