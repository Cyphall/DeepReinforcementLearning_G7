using Common.Core;
using Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Agent.MC
{
    /// <summary>
    /// Implémentation d'un processus de décision markovien suivant une stratégie d'actions précalculée
    /// </summary>
    /// <typeparam name="TGameState">Etat de jeu manipulé</typeparam>
    /// <typeparam name="TGameRules">Règles de jeu utilisés</typeparam>
    public class MCOnPolicyFirstVisitAgent<TGameState, TGameRules> : AAgent<TGameState, TGameRules>
        where TGameState : IGameState<TGameState>
        where TGameRules : AGameRules<TGameState>
    {
        #region Champs

        protected readonly float _devaluationFactor;

        protected readonly Dictionary<TGameState, List<AGameAction<TGameState>>> _gameStateToActions = new Dictionary<TGameState, List<AGameAction<TGameState>>>();
        
        protected readonly Dictionary<(TGameState, AGameAction<TGameState>), float> _stateActionPairToAccumulation = new Dictionary<(TGameState, AGameAction<TGameState>), float>();
        
        protected readonly Dictionary<(TGameState, AGameAction<TGameState>), float> _stateActionPairToValues = new Dictionary<(TGameState, AGameAction<TGameState>), float>();

        protected readonly Dictionary<(TGameState, AGameAction<TGameState>), int> _stateActionPairToVisits = new Dictionary<(TGameState, AGameAction<TGameState>), int>();

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
        public MCOnPolicyFirstVisitAgent(TGameRules rules, IGameAgentPlugin<TGameState> plugin, float devaluationFactor = 0.9f) : base(rules)
        {
            this._devaluationFactor = devaluationFactor;
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
            AGameAction<TGameState> action = this._gameStatePolicies[this._gameStates.IndexOf(gameState)];

            Debug.Log(action);
            return action;
        }

        /// <summary>
        /// Initialise l'agent avec l'état de jeu initial
        /// </summary>
        /// <param name="initialGameState">Etat de jeu initial</param>
        /// <param name="episodes">Nombre de parties à simuler</param>
        /// <param name="maxEpisodeLength">Taille maximum de déroulement d'un épisode</param>
        public void Initialize(TGameState initialGameState, int episodes = 30, int maxEpisodeLength = 20000)
        {
            this._gameStatePolicies.Clear();
            this._gameStates.Clear();
            this._gameStateValues.Clear();

            this.InitializePossibleStates(initialGameState);
            this.EvaluatePolicy(episodes, maxEpisodeLength);
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Evalue la stratégie utilisée
        /// </summary>
        /// <param name="episodes">Nombre de parties à simuler</param>
        /// <param name="maxEpisodeLength">Taille maximum de déroulement d'un épisode</param>
        private void EvaluatePolicy(int episodes, int maxEpisodeLength)
        {
            for (int i = 0; i < episodes; ++i)
            {
                (List<TGameState> gameStates, List<AGameAction<TGameState>> actions, List<float> rewards) = this.GenerateEpisode(maxEpisodeLength);
                float valueAccumulation = 0;

                for (int t = gameStates.Count - 2; t >= 0; --t)
                {
                    valueAccumulation += rewards[t + 1];

                    if (!gameStates.Take(t - 1).Contains(gameStates[t]))
                    {
                        if (!this._gameStateToActions[gameStates[t]].Contains(actions[t]))
                            this._gameStateToActions[gameStates[t]].Add(actions[t]);

                        (TGameState, AGameAction<TGameState>) stateActionPair = (gameStates[t], actions[t]);

                        this._stateActionPairToAccumulation[stateActionPair] += valueAccumulation;
                        this._stateActionPairToValues[stateActionPair] = this._stateActionPairToAccumulation[stateActionPair] / ++this._stateActionPairToVisits[stateActionPair];
                    }
                }

                foreach (TGameState gameState in gameStates)
                {
                    if (_gameStateToActions.ContainsKey(gameState))
                    {
                        List<AGameAction<TGameState>> gameStateActions = _gameStateToActions[gameState];
                        AGameAction<TGameState> bestAction = gameStateActions[0];
                        float bestValue = _stateActionPairToValues[(gameState, bestAction)];

                        foreach (AGameAction<TGameState> action in gameStateActions.Skip(1))
                        {
                            var pair = (gameState, action);

                            if (_stateActionPairToValues.ContainsKey(pair) && bestValue < _stateActionPairToValues[pair])
                            {
                                bestValue = _stateActionPairToValues[pair];
                                bestAction = action;
                            }
                        }

                        this._gameStatePolicies[this._gameStates.IndexOf(gameState)] = bestAction;
                    }
                }
            }
        }

        private (List<TGameState>, List<AGameAction<TGameState>>, List<float>) GenerateEpisode(int maxEpisodeLength)
        {
            List<TGameState> gameStates = new List<TGameState>();
            List<AGameAction<TGameState>> actions = new List<AGameAction<TGameState>>();
            List<float> rewards = new List<float>();

            int iterations = 0;
            TGameState currentGameState = this._gameStates[0];

            while (currentGameState.Status == GameStatus.Playing && iterations < maxEpisodeLength)
            {
                int index = this._gameStates.IndexOf(currentGameState);
                AGameAction<TGameState> action = this._gameStatePolicies[index];
                TGameState nextGameState = this._rules.Tick(action, currentGameState);

                gameStates.Add(currentGameState);
                actions.Add(action);
                rewards.Add(this.PolicyValue(currentGameState, action, nextGameState));

                currentGameState = this._gameStates[this._gameStates.IndexOf(nextGameState)];
                ++iterations;
            }

            return (gameStates, actions, rewards);
        }

        /// <summary>
        /// Calcule tous les états possibles à partir d'un état initial
        /// </summary>
        /// <param name="initialGameState">Etat de jeu initial</param>
        private void InitializePossibleStates(TGameState initialGameState)
        {
            List<AGameAction<TGameState>> actions = this._rules.GetPossibleActions(initialGameState);
            AGameAction<TGameState> gameStateAction = actions[Random.Range(0, actions.Count)];

            this._gameStatePolicies.Add(gameStateAction);
            this._gameStates.Add(initialGameState);
            this._gameStateValues.Add(0);

            (TGameState, AGameAction<TGameState>) stateActionPair = (initialGameState, gameStateAction);

            if (!_gameStateToActions.ContainsKey(initialGameState))
                _gameStateToActions.Add(initialGameState, new List<AGameAction<TGameState>>());

            if (!_stateActionPairToAccumulation.ContainsKey(stateActionPair))
            {
                _stateActionPairToAccumulation.Add(stateActionPair, 0f);
                _stateActionPairToValues.Add(stateActionPair, 0f);
                _stateActionPairToVisits.Add(stateActionPair, 0);
            }

            _gameStateToActions[initialGameState].Add(gameStateAction);
            _stateActionPairToAccumulation[stateActionPair] = 0f;
            _stateActionPairToValues[stateActionPair] = 0;
            ++_stateActionPairToVisits[stateActionPair];

            if (initialGameState.Status != GameStatus.Playing)
                return;

            foreach (AGameAction<TGameState> action in actions)
            {
                TGameState gameState = action.Apply(initialGameState.Copy());

                if (!this._gameStates.Contains(gameState))
                    this.InitializePossibleStates(gameState);
            }
        }

        /// <summary>
        /// Calcule la valeur pour une action appliquée en fonction d'un état de jeu
        /// </summary>
        /// <param name="gameState">Etat de jeu utilisé</param>
        /// <param name="policy">Action à appliquer</param>
        /// <param name="nextGameState">Etat de jeu suivant</param>
        /// <returns>La valeur de l'application de cette action</returns>
        private float PolicyValue(TGameState gameState, AGameAction<TGameState> policy, TGameState nextGameState)
        {
            int index = this._gameStates.IndexOf(nextGameState);
            var pair = (this._gameStates[index], policy);
            float value = 0f;

            if (this._stateActionPairToValues.ContainsKey(pair))
                value = this._stateActionPairToValues[pair];
            return this._plugin.TransitionReward(gameState, policy, nextGameState) + this._devaluationFactor * value;
        }

        #endregion
    }

    public class MCOnPolicyFirstVisitAgent<TGameState> : MCOnPolicyFirstVisitAgent<TGameState, AGameRules<TGameState>>
        where TGameState : IGameState<TGameState>
    {
        public MCOnPolicyFirstVisitAgent(AGameRules<TGameState> rules, IGameAgentPlugin<TGameState> plugin, float devaluationFactor = 0.9f) :
            base(rules, plugin, devaluationFactor)
        { }
    }
}
