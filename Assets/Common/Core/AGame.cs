using System;
using System.Collections.Generic;

namespace Common.Core
{
    public abstract class AGame<TGameState>
        where TGameState : ICloneable
    {
        #region Champs

        /// <summary>
        /// Obtient les actions possibles dans le jeu
        /// </summary>
        public readonly List<AGameAction<TGameState>> Actions;

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Fournit l'état de jeu suivant l'état de jeu fourni
        /// </summary>
        /// <param name="state">Etat de jeu actuel</param>
        /// <returns>Etat de jeu suivant l'actuel</returns>
        public TGameState Update(TGameState state, AGameAction<TGameState> action) => this.Actions.Contains(action) ? action.Apply((TGameState)state.Clone()) : state;

        #endregion

        #region Méthodes publiques abstraites

        /// <summary>
        /// Retourne la récompense de transiter d'un état de jeu au prochain par l'exécution d'une action
        /// </summary>
        /// <param name="state">Etat de jeu originel</param>
        /// <param name="action">Action à exécuter</param>
        /// <param name="nextGameState">Etat de jeu suivant crée par l'exécution de l'action</param>
        /// <returns>La récompense</returns>
        public abstract float GetReward(TGameState state, AGameAction<TGameState> action, TGameState nextGameState);

        /// <summary>
        /// Indique si l'état de jeu fourni est terminal
        /// </summary>
        /// <param name="state">Etat de jeu à analyser</param>
        /// <returns>TRUE si le jeu est fini, FALSE sinon</returns>
        public abstract bool IsFinished(TGameState state);

        #endregion
    }
}
