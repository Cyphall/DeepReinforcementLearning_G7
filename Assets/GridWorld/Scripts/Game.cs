using Common.Core;

namespace GridWorld
{
    public class Game : AGame<GameState>
    {
        public enum GameStatus
        {
            Playing,
            Win,
            Lose
        }
        
        public GameStatus GetStatus(GameState state)
        {
            return state.Grid[state.AgentPos.x, state.AgentPos.y] switch
            {
                TileType.Hole => GameStatus.Lose,
                TileType.Goal => GameStatus.Win,
                _ => GameStatus.Playing
            };
        }
    }
}