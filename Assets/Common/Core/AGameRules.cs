using System.Collections.Generic;

namespace Common.Core
{
    /// <summary>
    /// Fournit les règles d'un jeu et centralise les actions
    /// </summary>
    /// <typeparam name="TGameState">Etat de jeu manipulé</typeparam>
    public abstract class AGameRules<TGameState> where TGameState : IGameState<TGameState>
    {
        /// <summary>
        /// Retourne une liste d'actions possibles en fonction de l'état de jeu
        /// </summary>
        /// <param name="gameState">Etat de jeu à évaluer</param>
        /// <returns>La liste d'actions possibles pour cet état de jeu</returns>
        public abstract List<AGameAction<TGameState>> GetPossibleActions(TGameState gameState);

        /// <summary>
        /// Applique une action sur un état de jeu
        /// </summary>
        /// <param name="action">Action à appliquer</param>
        /// <param name="gameState">Etat de jeu à modifier</param>
        /// <returns>Un nouvel état de jeu</returns>
        public virtual TGameState Tick(AGameAction<TGameState> action, TGameState gameState) => action.Apply(gameState.Copy());
    }
}