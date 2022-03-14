using Common.Core;
using Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

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
        public MDPValueAgent(TGameRules rules, IGameAgentPlugin<TGameState> plugin, TGameState gameState, float devaluationFactor = 0.9f, float differenceThreshold = 0.25f) : base(rules)
        {
            this._devaluationFactor = devaluationFactor;
            this._differenceThreshold = differenceThreshold;
            this._plugin = plugin;
            
            
            //start
            StatsRecorder.TrainingStarted();
            this.Initialize(gameState);
            StatsRecorder.TrainingFinished();
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
            AGameAction<TGameState> action = this._gameStatePolicies[this._gameStates.IndexOf(gameState)];

            return action;
        }

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
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Evalue la stratégie utilisée
        /// </summary>
        private void EvaluatePolicy()
        {
            float delta;

            StatsRecorder.NewEvaluation();
            
            do
            {
                StatsRecorder.NewIteration();
                delta = 0f;

                for (int i = 0; i < this._gameStates.Count; ++i)
                {
                    TGameState gameState = this._gameStates[i];

                    if (gameState.Status == GameStatus.Playing)
                    {
                        float oldValue = this._gameStateValues[i];
                        this.Propagate(gameState, new List<TGameState>() { gameState });
                        delta = Math.Max(delta, Math.Abs(oldValue - this._gameStateValues[i]));
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

            this._gameStatePolicies.Add(actions[Random.Range(0, actions.Count)]);
            this._gameStates.Add(initialGameState);
            this._gameStateValues.Add(baseStateValue);

            if (initialGameState.Status != GameStatus.Playing)
                return;

            foreach (AGameAction<TGameState> action in actions)
            {
                TGameState gameState = this._rules.Tick(action, initialGameState);

                if (!this._gameStates.Contains(gameState))
                    this.InitializePossibleStates(gameState, baseStateValue);
            }
        }

        /// <summary>
        /// Propage la valeur d'un état de jeu pour toutes les actions possibles à jouer
        /// </summary>
        /// <param name="gameState">Etat de jeu utilisé</param>
        private void Propagate(TGameState gameState, List<TGameState> visitedStates)
        {
            int index = this._gameStates.IndexOf(gameState);

            if (gameState.Status != GameStatus.Playing)
            {
                this._gameStateValues[index] = this._plugin.Reward(gameState);
                return;
            }

            foreach (AGameAction<TGameState> action in this._rules.GetPossibleActions(gameState))
            {
                TGameState nextGameState = this._rules.Tick(action, gameState);

                if (!visitedStates.Contains(nextGameState))
                {
                    visitedStates.Add(nextGameState);
                    this.Propagate(nextGameState, visitedStates);
                }

                float value = this._plugin.TransitionReward(gameState, action, nextGameState) + this._devaluationFactor * this._gameStateValues[this._gameStates.IndexOf(nextGameState)];

                if (value > this._gameStateValues[index])
                {
                    this._gameStateValues[index] = value;
                    this._gameStatePolicies[index] = action;
                }
            }

        }

        #endregion
    }

    public class MDPValueAgent<TGameState> : MDPValueAgent<TGameState, AGameRules<TGameState>>
        where TGameState : IGameState<TGameState>
    {
        public MDPValueAgent(AGameRules<TGameState> rules, IGameAgentPlugin<TGameState> plugin, TGameState gameState, float devaluationFactor = 0.9f, float differenceThreshold = 0.25F) :
            base(rules, plugin, gameState, devaluationFactor, differenceThreshold) { }
    }
}
