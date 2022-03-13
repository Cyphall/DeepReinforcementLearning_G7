﻿using Common.Core;
using Common.Enumeration;
using System;
using System.Collections.Generic;

using Random = UnityEngine.Random;

namespace Common.Agent.DP
{
    /// <summary>
    /// Implémentation d'un processus de décision markovien suivant une stratégie d'actions précalculée
    /// </summary>
    /// <typeparam name="TGameState">Etat de jeu manipulé</typeparam>
    /// <typeparam name="TGameRules">Règles de jeu utilisés</typeparam>
    public class MDPPolicyAgent<TGameState, TGameRules> : AAgent<TGameState, TGameRules>
        where TGameState : IGameState<TGameState>
        where TGameRules : AGameRules<TGameState>
    {
        #region Champs

        protected readonly float _devaluationFactor;

        protected readonly float _differenceThreshold;
        
        protected readonly List<AGameAction<TGameState>> _gameStatePolicies = new List<AGameAction<TGameState>>();

        protected readonly List<TGameState> _gameStates = new List<TGameState>();

        protected readonly List<float> _gameStateValues = new List<float>();

        protected readonly IGameAgentPlugin<TGameState> _plugin;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="rules">Règles du jeu</param>
        /// <param name="plugin">Branchement possédant la stratégie de récompense utilisée</param>
        /// <param name="devaluationFactor">Facteur de dévaluation de l'évaluation de la politique</param>
        /// <param name="differenceThreshold">Seuil de différence pour l'évaluation de la politique</param>
        public MDPPolicyAgent(TGameRules rules, IGameAgentPlugin<TGameState> plugin, float devaluationFactor = 1f, float differenceThreshold = 0.25f) : base(rules)
        {
            this._devaluationFactor = devaluationFactor;
            this._differenceThreshold = differenceThreshold;
            this._plugin = plugin;
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Donne une action à jouer
        /// </summary>
        /// <param name="gameState">Etat de jeu actuel</param>
        /// <returns>L'action à jouer</returns>
        override public AGameAction<TGameState> GetAction(TGameState gameState) =>
            this._gameStatePolicies[this._gameStates.IndexOf(gameState)];

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
            this.EvaluatePolicy();

            // Fin de l'entraînement
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
            while (delta >= this._differenceThreshold);

            this.ImprovePolicy();
        }

        /// <summary>
        /// Améliore la stratégie actuelle
        /// </summary>
        private void ImprovePolicy()
        {
            bool stable = true;

            for (int i = 0; i < this._gameStates.Count; ++i)
            {
                TGameState gameState = this._gameStates[i];

                if (gameState.Status == GameStatus.Playing)
                {
                    AGameAction<TGameState> oldAction = this._gameStatePolicies[i];
                    float bestValue = this._gameStateValues[i];

                    AGameAction<TGameState> bestAction = oldAction;

                    foreach (AGameAction<TGameState> action in this._rules.GetPossibleActions(gameState))
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
            }

            if (!stable)
                this.EvaluatePolicy();
        }

        /// <summary>
        /// Calcule tous les états possibles à partir d'un état initial
        /// </summary>
        /// <param name="initialGameState">Etat de jeu initial</param>
        /// <param name="baseStateValue">Valeur de base pour l'initialisation des données d'un état de jeu</param>
        private void InitializePossibleStates(TGameState initialGameState, float baseStateValue)
        {
            List<AGameAction<TGameState>> actions = this._rules.GetPossibleActions(initialGameState);

            this._gameStatePolicies.Add(actions[Random.Range(0, actions.Count)]);
            this._gameStates.Add(initialGameState);
            this._gameStateValues.Add(baseStateValue);

            foreach (AGameAction<TGameState> action in actions)
            {
                TGameState gameState = action.Apply(initialGameState.Copy());

                if (!this._gameStates.Contains(gameState))
                    this.InitializePossibleStates(gameState, baseStateValue);
            }
        }

        /// <summary>
        /// Calcule la valeur pour une action appliquée en fonction d'un état de jeu
        /// </summary>
        /// <param name="gameState">Etat de jeu utilisé</param>
        /// <param name="policy">Action à appliquer</param>
        /// <returns>La valeur de l'application de cette action</returns>
        private float PolicyValue(TGameState gameState, AGameAction<TGameState> policy)
        {
            if (gameState.Status != GameStatus.Playing)
                return this._plugin.Reward(gameState);

            TGameState nextGameState = policy.Apply(gameState.Copy());
            return this._plugin.TransitionReward(gameState, policy, nextGameState) + this._devaluationFactor * this._gameStateValues[this._gameStates.IndexOf(nextGameState)];
        }

        #endregion
    }

    public class MDPPolicyAgent<TGameState> : MDPPolicyAgent<TGameState, AGameRules<TGameState>>
        where TGameState : IGameState<TGameState>
    {
        public MDPPolicyAgent(AGameRules<TGameState> rules, IGameAgentPlugin<TGameState> plugin, float gamma = 1, float theta = 0.25F) : base(rules, plugin, gamma, theta) { }
    }
}
