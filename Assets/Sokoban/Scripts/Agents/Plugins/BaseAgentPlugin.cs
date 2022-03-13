using Common.Core;
using Common.Enumeration;
using Sokoban.Game;

namespace Sokoban.Agent.Plugin
{
    public class BaseAgentPlugin : IGameAgentPlugin<GameState>
    {
        public float Reward(GameState gameState) => gameState.Status switch
        {
            GameStatus.Win => 1,
            GameStatus.Lose => -1,
            _ => 0,
        };

        public float TransitionReward(GameState gameState, AGameAction<GameState> action, GameState nextGameState) => this.Reward(nextGameState) + this.Reward(gameState);
    }
}
