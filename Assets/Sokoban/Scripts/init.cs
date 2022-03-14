using System.Collections;
using System.Collections.Generic;
using Sokoban;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

namespace Sokoban
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

    }
}