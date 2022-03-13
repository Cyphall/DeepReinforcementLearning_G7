using Common.Core;
using System.Collections.Generic;

using Random = UnityEngine.Random;

namespace Common.Agent
{
    public class RandomAgent<TGameState, TGameRules> : AAgent<TGameState, TGameRules>
        where TGameState : IGameState<TGameState>
        where TGameRules : AGameRules<TGameState>
    {
        #region Constructeur

        public RandomAgent(TGameRules rules) : base(rules) { }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Donne une action à jouer
        /// </summary>
        /// <param name="gameState">Etat de jeu actuel</param>
        /// <returns>L'action à jouer</returns>
        public override AGameAction<TGameState> GetAction(TGameState gameState)
        {
            List<AGameAction<TGameState>> actions = this._rules.GetPossibleActions(gameState);

            return actions[Random.Range(0, actions.Count)];
        }

        #endregion
    }
}
