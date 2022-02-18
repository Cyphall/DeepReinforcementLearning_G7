using Common.Core;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Common.Agent
{
    public class RandomAgent<TGameState> : AAgent<TGameState>
        where TGameState : IGameState<TGameState>
    {
        #region Constructeur

        public RandomAgent(List<AGameAction<TGameState>> actions) : base(actions) { }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Donne une action à jouer
        /// </summary>
        /// <param name="gameState">Etat de jeu actuel</param>
        /// <returns>L'action à jouer</returns>
        public override AGameAction<TGameState> GetAction(TGameState gameState) => this._actions[Random.Range(0, this._actions.Count)];

        #endregion
    }
}
