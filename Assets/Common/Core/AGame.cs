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

        #region M�thodes publiques

        /// <summary>
        /// Fournit l'�tat de jeu suivant l'�tat de jeu fourni
        /// </summary>
        /// <param name="state">Etat de jeu actuel</param>
        /// <returns>Etat de jeu suivant l'actuel</returns>
        public TGameState Update(TGameState state, AGameAction<TGameState> action) => this.Actions.Contains(action) ? action.Apply((TGameState)state.Clone()) : state;

        #endregion

        #region M�thodes publiques abstraites

        /// <summary>
        /// Retourne la r�compense de transiter d'un �tat de jeu au prochain par l'ex�cution d'une action
        /// </summary>
        /// <param name="state">Etat de jeu originel</param>
        /// <param name="action">Action � ex�cuter</param>
        /// <param name="nextGameState">Etat de jeu suivant cr�e par l'ex�cution de l'action</param>
        /// <returns>La r�compense</returns>
        public abstract float GetReward(TGameState state, AGameAction<TGameState> action, TGameState nextGameState);

        /// <summary>
        /// Indique si l'�tat de jeu fourni est terminal
        /// </summary>
        /// <param name="state">Etat de jeu � analyser</param>
        /// <returns>TRUE si le jeu est fini, FALSE sinon</returns>
        public abstract bool IsFinished(TGameState state);

        #endregion
    }
}
