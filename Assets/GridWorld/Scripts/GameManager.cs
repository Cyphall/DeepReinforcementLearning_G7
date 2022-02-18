using System.Collections.Generic;
using Common.Core;
using UnityEngine;
using UnityEngine.UI;

namespace GridWorld
{
    public class GameManager : MonoBehaviour
    {
        public List<TileToPrefab> TileToPrefabs;
        public GameObject playerPrefab;
        public ALevelPreset _levelPreset;
        public GameObject displayText;
        private PlayerScript _player;

        /// <summary>
        /// Stocke l'état du jeu actuel
        /// </summary>
        public GameState GameState { get; private set; }

        private void Start()
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
        
        public GameStatus GetStatus()
        {
            return GameState.Grid[GameState.AgentPos.x, GameState.AgentPos.y] switch
            {
                TileType.Hole => GameStatus.Lose,
                TileType.Goal => GameStatus.Win,
                _ => GameStatus.Playing
            };
        }

        public void FixedUpdate()
        {
            if (GetStatus() != GameStatus.Playing)
            {
                //c la f1 du mond
            }
        }

        public void ApplyAction(AGameAction<GameState> action)
        {
            GameState = action.Apply(GameState.Copy());
        }

        public void Update()
        {
            Vector2Int agentPos = GameState.AgentPos;
            _player.transform.position = new Vector3(agentPos.x, _player.transform.position.y, agentPos.y);
            if(GetStatus() == GameStatus.Win)
                displayText.SetActive(true);
            else if (GetStatus() == GameStatus.Lose)
                displayText.GetComponent<Text>().text = "Agent Lost";
        }
    }
}