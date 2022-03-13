namespace Common.Core
{
    /// <summary>
    /// Représente un branchement entre les règles d'un jeu et un type d'agent
    /// </summary>
    /// <typeparam name="TGameState">Etat de jeu manipulé</typeparam>
    public interface IGameAgentPlugin<TGameState> where TGameState : IGameState<TGameState>
    {
        /// <summary>
        /// Retourne une récompense en fonction d'un état de jeu
        /// </summary>
        /// <param name="gameState">Etat de jeu à évaluer</param>
        /// <returns>La récompense obtenue</returns>
        public float Reward(TGameState gameState);

        /// <summary>
        /// Retourne la récompense de passage d'un état à un autre par une action
        /// </summary>
        /// <param name="gameState">Etat de jeu initial</param>
        /// <param name="action">Action effectuée</param>
        /// <param name="nextGameState">Etat de jeu après l'action</param>
        /// <returns>La récompense de transition</returns>
        public float TransitionReward(TGameState gameState, AGameAction<TGameState> action, TGameState nextGameState);
    }
}