using Common.Core;

namespace Common.Agent.TD
{
    public class SARSAAgent<TGameState, TGameRules> : AAgent<TGameState, TGameRules>
        where TGameState : IGameState<TGameState>
        where TGameRules : AGameRules<TGameState>
    {
        #region Champs

        private readonly IGameAgentPlugin<TGameState> _plugin;

        #endregion

        #region Constructeur

        public SARSAAgent(TGameRules rules, IGameAgentPlugin<TGameState> plugin) : base(rules)
        {
            this._plugin = plugin;
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
