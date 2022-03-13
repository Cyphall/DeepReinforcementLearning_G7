using Common.Core;
using GridWorld.Actions;
using System.Collections.Generic;

namespace GridWorld.Game
{
    public sealed class GameRules : AGameRules<GameState>
    {
        #region Actions

        public readonly MoveDown MoveDown = new MoveDown();
        public readonly MoveLeft MoveLeft = new MoveLeft();
        public readonly MoveRight MoveRight = new MoveRight();
        public readonly MoveUp MoveUp = new MoveUp();
        public readonly Wait Wait = new Wait();

        public readonly List<AGameAction<GameState>> EveryActions;

        #endregion

        #region Constructeur

        public GameRules()
        {
            this.EveryActions = new List<AGameAction<GameState>>
            {
                this.MoveDown,
                this.MoveLeft,
                this.MoveRight,
                this.MoveUp,
                this.Wait,
            };
        }

        #endregion

        #region Méthodes publiques

        public override List<AGameAction<GameState>> GetPossibleActions(GameState gameState) => this.EveryActions;

        #endregion
    }
}