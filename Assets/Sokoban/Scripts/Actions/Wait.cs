using Sokoban.Game;

namespace Sokoban.Actions
{
    public class Wait : Move
    {
        public override GameState Apply(GameState state)
        {
            return state;
        }
    }
}