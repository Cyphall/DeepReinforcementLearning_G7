using Common.Core;
using System.Collections.Generic;
using System.Linq;

using Random = UnityEngine.Random;

namespace Common.Agent
{
    public struct StateData<TGameState>
        where TGameState : AGameState<TGameState>
    {
        #region Propriétés

        /// <summary>
        /// Obtient ou définit l'action à jouer pour cet état de jeu
        /// </summary>
        public AGameAction<TGameState> Action
        {
            get;
            set;
        }

        /// <summary>
        /// Obtient l'état de jeu désigné
        /// </summary>
        public readonly TGameState GameState
        {
            get;
        }

        /// <summary>
        /// Obtient la valeur de l'état de jeu
        /// </summary>
        public float Value
        {
            get;
            set;
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="gameState">Etat de jeu</param>
        /// <param name="value">Valeur de base de l'état de jeu</param>
        public StateData(AGameAction<TGameState> action, TGameState gameState, float value)
        {
            this.Action = action;
            this.GameState = gameState;
            this.Value = value;
        }

        #endregion
    }

    public class MDPAgent<TGame, TGameState> : AAgent<TGame, TGameState>
        where TGame : AGame<TGameState>
        where TGameState : AGameState<TGameState>
    {
        #region Champs

        private List<StateData<TGameState>> _stateData = new List<StateData<TGameState>>();

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="game">Jeu à utiliser</param>
        public MDPAgent(TGame game, TGameState initialGameState, float baseStateValue = 0f) : base(game)
        {
            this.InitializePossibleStates(initialGameState, baseStateValue);
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Donne une action à jouer
        /// </summary>
        /// <param name="state">Etat de jeu actuel</param>
        /// <returns>L'action à jouer</returns>
        override public AGameAction<TGameState> GetAction(TGameState state)
        {
            return this._game.Actions[0];
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Evalue la stratégie utilisée
        /// </summary>
        private void EvaluatePolicy()
        {

        }

        /// <summary>
        /// Améliore la stratégie actuelle
        /// </summary>
        private void ImprovePolicy()
        {

        }

        /// <summary>
        /// Calcule tous les états possibles à partir d'un état initial
        /// </summary>
        /// <param name="initialGameState">Etat de jeu initial</param>
        /// <param name="baseStateValue">Valeur de base pour l'initialisation des données d'un état de jeu</param>
        private void InitializePossibleStates(TGameState initialGameState, float baseStateValue)
        {
            this._stateData.Add(new StateData<TGameState>(this._game.Actions[Random.Range(0, this._game.Actions.Count)], initialGameState, baseStateValue));

            foreach (AGameAction<TGameState> action in this._game.Actions)
            {
                TGameState gameState = this._game.Update(initialGameState, action);

                if (!this._stateData.Any(data => data.GameState.Equals(gameState)))
                    this.InitializePossibleStates(gameState, baseStateValue);
            }
        }

        #endregion
    }
}
