using System;

namespace Common.Core
{
    public abstract class AAgent<TGameState>
        where TGameState : AGameState<TGameState>
    {

        #region Constructeur

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="game">Jeu à utiliser</param>
        protected AAgent()
        {
           
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Restaure l'état par défaut de l'agent
        /// </summary>
        public virtual void Reset() { }

        #endregion

        #region Méthodes publiques abstraites

        /// <summary>
        /// Donne une action à jouer
        /// </summary>
        /// <param name="state">Etat de jeu actuel</param>
        /// <returns>L'action à jouer</returns>
        public abstract AGameAction<TGameState> GetAction(TGameState state);

        #endregion
    }
}
