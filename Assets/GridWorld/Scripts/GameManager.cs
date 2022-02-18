using System.Collections.Generic;
using Common.Agent.MDP;
using Common.Core;
using Common.Enumeration;
using UnityEngine;

namespace GridWorld
{
    public class GameManager : MonoBehaviour
    {
        public List<TileToPrefab> TileToPrefabs;
        public GameObject playerPrefab;
        public ALevelPreset _levelPreset;

        private PlayerScript _player;

        /// <summary>
        /// Stocke l'état du jeu actuel
        /// </summary>
        public GameState GameState { get; private set; }

        private void Awake()
        {
            for (int x = 0; x < _levelPreset.Grid.GetLength(0); ++x)
            {
                for (int y = 0; y < _levelPreset.Grid.GetLength(1); ++y)
                {
                    TileType tileType = _levelPreset.Grid[x, y];
                    foreach (var tile in TileToPrefabs)
                    {

                        if (tile.TileType == tileType)
                        {
                            Instantiate(tile.Prefab, new Vector3(x, tile.Prefab.transform.position.y, y), Quaternion.identity);
                        }
                    }
                }
            }
            _player = Instantiate(playerPrefab, new Vector3(_levelPreset.StartPosition.x, playerPrefab.transform.position.y, _levelPreset.StartPosition.y), Quaternion.identity).GetComponent<PlayerScript>();
            _player.GameManager = this;
            GameState = new GameState
            {
                Grid = _levelPreset.Grid,
                AgentPos = _levelPreset.StartPosition
            };
            
            int width = GameState.Grid.GetLength(0);
            int height = GameState.Grid.GetLength(1);

            Camera.main.transform.position = new Vector3(width / 2.0f, 10, height / 2.0f);
        }
        
        public void FixedUpdate()
        {

        }

        public void ApplyAction(AGameAction<GameState> action)
        {
            if (GameState.Status == GameStatus.Playing)
                GameState = action.Apply(GameState.Copy());
        }

        public void Update()
        {
            Vector2Int agentPos = GameState.AgentPos;
            _player.transform.position = new Vector3(agentPos.x, _player.transform.position.y, agentPos.y);
        }
    }
}