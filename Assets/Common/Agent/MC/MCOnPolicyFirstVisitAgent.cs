using Common.Core;
using Common.Enumeration;
using System.Collections.Generic;
using System.Linq;

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

        protected readonly List<AGameAction<TGameState>> _gameStatePolicies = new List<AGameAction<TGameState>>();

        protected readonly List<TGameState> _gameStates = new List<TGameState>();

        protected readonly List<float> _gameStateValues = new List<float>();

        protected readonly List<int> _gameStateVisits = new List<int>();

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
        public MCOnPolicyFirstVisitAgent(TGameRules rules, IGameAgentPlugin<TGameState> plugin, float devaluationFactor = 0.9f) : base(rules)
        {
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

            return action;
        }

        /// <summary>
        /// Initialise l'agent avec l'état de jeu initial
        /// </summary>
        /// <param name="initialGameState">Etat de jeu initial</param>
        /// <param name="episodes">Nombre de parties à simuler</param>
        public void Initialize(TGameState initialGameState, int episodes)
        {
            this._gameStatePolicies.Clear();
            this._gameStates.Clear();
            this._gameStateValues.Clear();

            this.InitializePossibleStates(initialGameState);
            this.EvaluatePolicy(episodes);
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Evalue la stratégie utilisée
        /// </summary>
        /// <param name="episodes">Nombre de parties à simuler</param>
        private void EvaluatePolicy(int episodes)
        {
            for (int i = 0; i < episodes; ++i)
            {
                (List<TGameState> states, List<float> rewards) = this.GenerateEpisode();
                float valueAccumulation = 0;

                for (int t = states.Count - 1; t >= 0; --t)
                {
                    valueAccumulation += rewards[t + 1];

                    if (!states.Take(t).Contains(states[t + 1]))
                    {
                        int index = this._gameStates.IndexOf(states[t + 1]);

                        this._gameStateValues[index] += valueAccumulation;
                        ++this._gameStateVisits[index];
                    }
                }
            }

            for (int i = 0; i < this._gameStates.Count; ++i)
                this._gameStateValues[i] /= this._gameStateVisits[i];
        }

        private (List<TGameState>, List<float>) GenerateEpisode()
        {
            List<TGameState> states = new List<TGameState>();
            List<float> rewards = new List<float>();

            TGameState currentGameState = this._gameStates[0];

            while (currentGameState.Status == GameStatus.Playing)
            {
                int index = this._gameStates.IndexOf(currentGameState);
                AGameAction<TGameState> action = this._gameStatePolicies[index];
                TGameState nextGameState = this._rules.Tick(action, currentGameState);

                states.Add(currentGameState);
                rewards.Add(this.PolicyValue(currentGameState, action, nextGameState));

                currentGameState = nextGameState;
            }

            return (states, rewards);
        }

        /// <summary>
        /// Calcule tous les états possibles à partir d'un état initial
        /// </summary>
        /// <param name="initialGameState">Etat de jeu initial</param>
        private void InitializePossibleStates(TGameState initialGameState)
        {
            List<AGameAction<TGameState>> actions = this._rules.GetPossibleActions(initialGameState);

            this._gameStatePolicies.Add(actions[Random.Range(0, actions.Count)]);
            this._gameStates.Add(initialGameState);
            this._gameStateValues.Add(0);
            this._gameStateVisits.Add(0);

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
            return this._plugin.TransitionReward(gameState, policy, nextGameState) + this._devaluationFactor * this._gameStateValues[this._gameStates.IndexOf(nextGameState)];
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
