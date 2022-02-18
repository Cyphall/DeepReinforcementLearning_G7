using Common.Core;
using Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;

using Random = UnityEngine.Random;

namespace Common.Agent.MDP
{
    public class MDPAgent<TGameState> : AAgent<TGameState>
        where TGameState : AGameState<TGameState>
    {
        #region Champs

        private float _gamma;

        private float _theta;

        private List<AGameAction<TGameState>> _gameStatePolicies = new List<AGameAction<TGameState>>();

        private List<TGameState> _gameStates = new List<TGameState>();

        private List<float> _gameStateValues = new List<float>();

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="actions">Actions possibles dans le jeu</param>
        /// <param name="gamma">Facteur de dévaluation de l'évaluation de la politique</param>
        /// <param name="theta">Limite de différence pour l'évaluation de la politique</param>
        public MDPAgent(List<AGameAction<TGameState>> actions, float gamma = 1f, float theta = 0.25f) : base(actions)
        {
            this._gamma = gamma;
            this._theta = theta;
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Donne une action à jouer
        /// </summary>
        /// <param name="gameState">Etat de jeu actuel</param>
        /// <returns>L'action à jouer</returns>
        override public AGameAction<TGameState> GetAction(TGameState gameState) => this._gameStatePolicies[this._gameStates.IndexOf(gameState)];

        /// <summary>
        /// Initialise l'agent avec l'état de jeu initial
        /// </summary>
        /// <param name="initialGameState">Etat de jeu initial</param>
        /// <param name="baseStateValue">Valeur d'initialisation des états de jeu</param>
        public void Initialize(TGameState initialGameState, float baseStateValue = 0f)
        {
            this._gameStatePolicies.Clear();
            this._gameStates.Clear();
            this._gameStateValues.Clear();

            this.InitializePossibleStates(initialGameState, baseStateValue);
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Evalue la stratégie utilisée
        /// </summary>
        private void EvaluatePolicy()
        {
            float delta;

            do
            {
                delta = 0f;

                for (int i = 0; i < this._gameStatePolicies.Count; ++i)
                {
                    TGameState gameState = this._gameStates[i];

                    if (gameState.Status == GameStatus.Playing)
                    {
                        float value = this._gameStateValues[i];
                        this._gameStateValues[i] = this.PolicyValue(gameState, this._gameStatePolicies[i]);
                        delta = Math.Max(delta, Math.Abs(value - this._gameStateValues[i]));
                    }
                }
            }
            while (delta < this._theta);
        }

        /// <summary>
        /// Améliore la stratégie actuelle
        /// </summary>
        private void ImprovePolicy()
        {
            bool stable = true;

            for (int i = 0; i < this._gameStates.Count; ++i)
            {
                AGameAction<TGameState> oldAction = this._gameStatePolicies[i];
                TGameState gameState = this._gameStates[i];
                float bestValue = this._gameStateValues[i];
                
                AGameAction<TGameState> bestAction = oldAction;

                foreach (AGameAction<TGameState> action in this._actions)
                {
                    float value = this.PolicyValue(gameState, action);

                    if (value > bestValue)
                    {
                        bestValue = value;
                        bestAction = action;
                    }
                }

                this._gameStatePolicies[i] = bestAction;

                if (oldAction != this._gameStatePolicies[i])
                    stable = false;
            }

            if (!stable) this.EvaluatePolicy();
        }

        /* A METTRE DANS LE PLUGIN */

        private float GetRewardFromState(TGameState gameState) => gameState.Status switch
        {
            GameStatus.Win => 1,
            GameStatus.Lose => -1,
            _ => 0,
        };

        private float Reward(TGameState gameState, TGameState nextGameState) => this.GetRewardFromState(nextGameState) + this.GetRewardFromState(gameState);

        /* JUSQUE LA */

        private float PolicyValue(TGameState gameState, AGameAction<TGameState> policy)
        {
            TGameState nextGameState = policy.Apply(gameState.Copy());
            int nextPolicyIndex = this._gameStates.IndexOf(nextGameState);

            return this.Reward(gameState, nextGameState) + this._gamma * this.PolicyValue(nextGameState, this._gameStatePolicies[nextPolicyIndex]);
        }

        /// <summary>
        /// Calcule tous les états possibles à partir d'un état initial
        /// </summary>
        /// <param name="initialGameState">Etat de jeu initial</param>
        /// <param name="baseStateValue">Valeur de base pour l'initialisation des données d'un état de jeu</param>
        private void InitializePossibleStates(TGameState initialGameState, float baseStateValue)
        {
            this._gameStatePolicies.Add(this._actions[Random.Range(0, this._actions.Count)]);
            this._gameStates.Add(initialGameState);
            this._gameStateValues.Add(baseStateValue);

            foreach (AGameAction<TGameState> action in this._actions)
            {
                TGameState gameState = action.Apply(initialGameState.Copy());

                if (!this._gameStates.Contains(gameState))
                    this.InitializePossibleStates(gameState, baseStateValue);
            }
        }

        #endregion
    }
}
