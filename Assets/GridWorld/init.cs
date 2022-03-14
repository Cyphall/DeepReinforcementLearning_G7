using System.Collections;
using System.Collections.Generic;
using Sokoban;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GridWorld
{
    public class init : MonoBehaviour
    {
        public GameObject GameManager;

        public GameObject displayText;

        // Start is called before the first frame update
        void Start()
        {
            GameObject gameManager = Instantiate(GameManager);
            gameManager.GetComponent<GameManager>().displayText = displayText;
        }

        public void ToInitScene()
        {
            SceneManager.LoadScene(0);
        }

    }
}