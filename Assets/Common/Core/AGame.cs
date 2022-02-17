using System;
using System.Collections.Generic;

namespace Common.Core
{
    public abstract class AGame<TGameState>
        where TGameState : AGameState<TGameState>
    {
        #region Champs

        /// <summary>
        /// Obtient les actions possibles dans le jeu
        /// </summary>
        public List<AGameAction<TGameState>> Actions { get; } = new List<AGameAction<TGameState>>();

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Fournit l'état de jeu suivant l'état de jeu fourni
        /// </summary>
        /// <param name="state">Etat de jeu actuel</param>
        /// <returns>Etat de jeu suivant l'actuel</returns>
        public TGameState Update(TGameState state, AGameAction<TGameState> action) => this.Actions.Contains(action) ? action.Apply(state.Copy()) : state;

        #endregion
    }
}
