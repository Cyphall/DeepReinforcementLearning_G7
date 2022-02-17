using Common.Core;
using System;

using Random = UnityEngine.Random;

namespace Common.Agent
{
    public sealed class RandomAgent<TGame, TGameState> : AAgent<TGame, TGameState>
        where TGame : AGame<TGameState>
        where TGameState : AGameState<TGameState>
    {
        #region Constructeur

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="game">Jeu à utiliser</param>
        public RandomAgent(TGame game) : base(game) { }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Donne une action à jouer
        /// </summary>
        /// <param name="state">Etat de jeu actuel</param>
        /// <returns>L'action à jouer</returns>
        public override AGameAction<TGameState> GetAction(TGameState state) => this._game.Actions[Random.Range(0, this._game.Actions.Count)];

        #endregion
    }
}
