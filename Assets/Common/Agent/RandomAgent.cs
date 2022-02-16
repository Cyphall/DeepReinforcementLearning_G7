using System;

using Random = UnityEngine.Random;

namespace Common.Agent
{
    public sealed class RandomAgent<TGame, TGameState> : AAgent<TGame, TGameState>
        where TGame : AGame<TGameState>
        where TGameState : ICloneable
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
        /// <returns>L'action à jouer</returns>
        override public AGameAction<TGameState> GetAction() => this._game.Actions[Random.Range(0, this._game.Actions.Length)];

        /// <summary>
        /// Restaure l'état par défaut de l'agent
        /// </summary>
        public override void Reset()
        {
        }

        #endregion
    }
}
