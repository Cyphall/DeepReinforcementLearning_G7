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
        /// <param name="game">Jeu � utiliser</param>
        public AAgent(TGame game)
        {
            this._game = game;
        }

        #endregion

        #region M�thodes publiques abstraites

        /// <summary>
        /// Donne une action � jouer
        /// </summary>
        /// <returns>L'action � jouer</returns>
        public abstract AGameAction<TGameState> GetAction();

        /// <summary>
        /// Restaure l'�tat par d�faut de l'agent
        /// </summary>
        public abstract void Reset();

        #endregion
    }
}
