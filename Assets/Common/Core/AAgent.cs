namespace Common.Core
{
    /// <summary>
    /// Classe abstraite représentant un agent de jeu
    /// </summary>
    /// <typeparam name="TGameState">Type d'état du jeu utilisé</typeparam>
    /// <typeparam name="TGameRules">Règles de jeu utilisées</typeparam>
    public abstract class AAgent<TGameState, TGameRules>
        where TGameState : IGameState<TGameState>
        where TGameRules : AGameRules<TGameState>
    {
        #region Champs

        protected readonly TGameRules _rules;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="rules">Règles du jeu</param>
        public AAgent(TGameRules rules)
        {
            this._rules = rules;
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
