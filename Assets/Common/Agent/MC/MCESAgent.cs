using Common.Core;

namespace Common.Agent.MC
{
    /// <summary>
    /// Implémentation de l'algorithme de Monte-Carlo
    /// </summary>
    /// <typeparam name="TGameState"></typeparam>
    /// <typeparam name="TGameRules"></typeparam>
    public class MCESAgent<TGameState, TGameRules> : AAgent<TGameState, TGameRules>
        where TGameState : IGameState<TGameState>
        where TGameRules : AGameRules<TGameState>
    {
        #region Constructeur

        public MCESAgent(TGameRules rules, IGameAgentPlugin<TGameState> plugin) : base(rules)
        {

        }

        #endregion

        #region Méthodes publiques

        public override AGameAction<TGameState> GetAction(TGameState gameState)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
