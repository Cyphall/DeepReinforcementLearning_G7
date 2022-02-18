using System.Collections.Generic;
using Common.Core;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

namespace Sokoban
{
    public class GameManager : MonoBehaviour
    {
        public List<TileToPrefab> TileToPrefabs;
        public GameObject playerPrefab;
        public GameObject cratePrefab;
        public ALevelPreset LevelPreset;
        public GameObject displayText;

        private PlayerScript _player;
        private List<GameObject> _crateInstances = new List<GameObject>();
        private ALevelPreset _levelPreset;
        /// <summary>
        /// Stocke l'état du jeu actuel
        /// </summary>
        public GameState GameState { get; private set; }

        private void Start()
        {
            _levelPreset = Instantiate(LevelPreset, new Vector3(0, 0, 0), Quaternion.identity);
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

            foreach (var position in _levelPreset.CrateStartPosition)
            {
                _crateInstances.Add(Instantiate(cratePrefab, new Vector3(position.x, cratePrefab.transform.position.y, position.y),
                    Quaternion.identity));
            }
            _player = Instantiate(playerPrefab, new Vector3(_levelPreset.StartPosition.x, playerPrefab.transform.position.y, _levelPreset.StartPosition.y), Quaternion.identity).GetComponent<PlayerScript>();
            _player.GameManager = this;
            GameState = new GameState
            {
                Grid = _levelPreset.Grid,
                AgentPos = _levelPreset.StartPosition,
                CratePos =  _levelPreset.CrateStartPosition
            };
            
            int width = GameState.Grid.GetLength(0);
            int height = GameState.Grid.GetLength(1);

            Camera.main.transform.position = new Vector3(width / 2.0f, 10, height / 2.0f);
        }

        public bool VerifyWin()
        {
            Vector2Int[] buttonsPos = GameState.GetAllButtonsPos();
            int nbValidButtons = 0;
            foreach (var buttonPos in buttonsPos)
            {
                foreach (var cratePos in GameState.CratePos)
                {
                    if (cratePos == buttonPos)
                        nbValidButtons++;
                }
            }

            if (nbValidButtons == buttonsPos.Length)
                return true;
            return false;
        }
        public GameStatus GetStatus()
        {
            if (GameState.CurrentIteration == GameState.NbMaxIteration)
                return GameStatus.Lose;

            if(VerifyWin())
                return GameStatus.Win;

            return GameStatus.Playing;
        }

        public void ApplyAction(AGameAction<GameState> action)
        {
            GameState = action.Apply(GameState.Copy());
        }

        public void Update()
        {
            Vector2Int agentPos = GameState.AgentPos;
            _player.transform.position = new Vector3(agentPos.x, _player.transform.position.y, agentPos.y);

            for (int i = 0; i < GameState.CratePos.Length; ++i)
            {
                _crateInstances[i].transform.position = new Vector3(GameState.CratePos[i].x,
                    _crateInstances[i].transform.position.y, GameState.CratePos[i].y);
            }
            if(GetStatus() == GameStatus.Win)
                displayText.SetActive(true);
            if (GetStatus() == GameStatus.Lose)
            {
                displayText.GetComponent<Text>().text = "Agent lose";
                displayText.SetActive(true);
            }
        }
    }
}