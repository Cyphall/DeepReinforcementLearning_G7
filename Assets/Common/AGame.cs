using System;

namespace Common
{
    public abstract class AGame<TGameState>
        where TGameState : ICloneable
    {
        /// <summary>
        /// Obtient les actions possibles dans le jeu
        /// </summary>
        public AGameAction<TGameState>[] Actions
        {
            get;
        }

        /// <summary>
        /// Fournit l'état de jeu suivant l'état de jeu fourni
        /// </summary>
        /// <param name="state">Etat de jeu actuel</param>
        /// <returns>Etat de jeu suivant l'actuel</returns>
        public TGameState Update(TGameState state, AGameAction<TGameState> action) => action.Apply((TGameState)state.Clone());
    }
}
