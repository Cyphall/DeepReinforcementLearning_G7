using Common.Core;

namespace Common.Agent.MC
{
    public class MCOnPolicyAgent<TGameState, TGameRules> : AAgent<TGameState, TGameRules>
        where TGameState : IGameState<TGameState>
        where TGameRules : AGameRules<TGameState>
    {
        #region Constructeur

        public MCOnPolicyAgent(TGameRules rules, IGameAgentPlugin<TGameState> plugin) : base(rules)
        {
        }

        #endregion

        #region Méthodes publiques

        public override AGameAction<TGameState> GetAction(TGameState gameState)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Méthodes privées

        private void Evaluate(AGameAction<TGameState> policy, int episodes)
        {

        }

        #endregion
    }
}
