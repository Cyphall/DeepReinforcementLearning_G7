using System;
using System.Collections.Generic;

namespace Common.Core
{
    public abstract class AAgent<TGameState>
        where TGameState : IGameState<TGameState>
    {
        #region Champs

        protected readonly List<AGameAction<TGameState>> _actions;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public AAgent(List<AGameAction<TGameState>> actions)
        {
            this._actions = actions;
        }

        #endregion

        #region Méthodes publiques abstraites

        /// <summary>
        /// Donne une action à jouer
        /// </summary>
        /// <param name="gameState">Etat de jeu actuel</param>
        /// <returns>L'action à jouer</returns>
        public abstract AGameAction<TGameState> GetAction(TGameState gameState);

        #endregion
    }
}
