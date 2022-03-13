﻿using Common.Core;
using Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Agent.DP
{
    /// <summary>
    /// Implémentation d'un processus de décision markovien suivant une stratégie de valeur optimale par action
    /// </summary>
    /// <typeparam name="TGameState">Etat de jeu manipulé</typeparam>
    /// <typeparam name="TGameRules">Règles de jeu utilisés</typeparam>
    public class MDPValueAgent<TGameState, TGameRules> : AAgent<TGameState, TGameRules>
        where TGameState : IGameState<TGameState>
        where TGameRules : AGameRules<TGameState>
    {
        #region Champs

        protected readonly float _devaluationFactor;

        protected readonly float _differenceThreshold;
        
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
        public MDPValueAgent(TGameRules rules, IGameAgentPlugin<TGameState> plugin, float devaluationFactor = 1f, float differenceThreshold = 0.25f) : base(rules)
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
        override public AGameAction<TGameState> GetAction(TGameState gameState)
        {
            List<AGameAction<TGameState>> actions = this._rules.GetPossibleActions(gameState);

            if (actions.Count <= 1)
                return actions.Count == 0 ? null : actions[0];

            AGameAction<TGameState> bestAction = actions[0];
            float bestValue = this.PolicyValue(gameState, bestAction);

            foreach (AGameAction<TGameState> action in actions.Skip(1))
            {
                float value = this.PolicyValue(gameState, action);

                if (bestValue < value)
                {
                    bestValue = value;
                    bestAction = action;
                }
            }

            return bestAction;
        }

        /// <summary>
        /// Initialise l'agent avec l'état de jeu initial
        /// </summary>
        /// <param name="initialGameState">Etat de jeu initial</param>
        /// <param name="baseStateValue">Valeur d'initialisation des états de jeu</param>
        public void Initialize(TGameState initialGameState, float baseStateValue = 0f)
        {
            this._gameStates.Clear();
            this._gameStateValues.Clear();

            this.InitializePossibleStates(initialGameState, baseStateValue);
            this.EvaluatePolicy();
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

                for (int i = 0; i < this._gameStates.Count; ++i)
                {
                    TGameState gameState = this._gameStates[i];

                    if (gameState.Status == GameStatus.Playing)
                    {
                        float oldValue = this._gameStateValues[i];
                        float bestValue = oldValue;

                        foreach (AGameAction<TGameState> action in this._rules.GetPossibleActions(gameState))
                        {
                            float value = this.PolicyValue(gameState, action);

                            if (value > bestValue)
                                bestValue = value;
                        }

                        this._gameStateValues[i] = bestValue;
                        delta = Math.Max(delta, Math.Abs(oldValue - bestValue));
                    }
                }
            }
            while (delta >= this._differenceThreshold);
        }

        /// <summary>
        /// Calcule tous les états possibles à partir d'un état initial
        /// </summary>
        /// <param name="initialGameState">Etat de jeu initial</param>
        /// <param name="baseStateValue">Valeur de base pour l'initialisation des données d'un état de jeu</param>
        private void InitializePossibleStates(TGameState initialGameState, float baseStateValue)
        {
            List<AGameAction<TGameState>> actions = this._rules.GetPossibleActions(initialGameState);

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

    public class MDPValueAgent<TGameState> : MDPValueAgent<TGameState, AGameRules<TGameState>>
        where TGameState : IGameState<TGameState>
    {
        public MDPValueAgent(AGameRules<TGameState> rules, IGameAgentPlugin<TGameState> plugin, float gamma = 1, float theta = 0.25F) : base(rules, plugin, gamma, theta) { }
    }
}