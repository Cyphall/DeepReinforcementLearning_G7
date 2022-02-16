using System;

namespace Common
{
    public abstract class AAgent<TGame, TGameState>
        where TGame : AGame<TGameState>
        where TGameState : ICloneable
    {
        #region Champs

        protected TGame _game;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="game">Jeu à utiliser</param>
        public AAgent(TGame game)
        {
            this._game = game;
        }

        #endregion

        #region Méthodes publiques abstraites

        /// <summary>
        /// Donne une action à jouer
        /// </summary>
        /// <returns>L'action à jouer</returns>
        public abstract AGameAction<TGameState> GetAction();

        /// <summary>
        /// Restaure l'état par défaut de l'agent
        /// </summary>
        public abstract void Reset();

        #endregion
    }
}
