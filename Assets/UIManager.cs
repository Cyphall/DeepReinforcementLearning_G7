using System;
using System.Collections;
using System.Collections.Generic;
using Common.Enumeration;
using Sokoban;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Algorithms _algorithm;

    public List<Sokoban.ALevelPreset> SokobanLevels;
    public List<GridWorld.ALevelPreset> GridWorldLevels;

    public Text levelText;
    public Dropdown AgentDropDown;
    public Dropdown GameDropDown;
    public Dropdown LevelDropDown;

    private List<Dropdown.OptionData> _sokoBanOptions;
    private List<Dropdown.OptionData> _gridWorldOptions;

    public GameManager SokobanGameManager;
    public GridWorld.GameManager GridWorldGameManager;
    void Awake()
    {
	    _sokoBanOptions = new List<Dropdown.OptionData>();
	    foreach (var level in SokobanLevels)
	    {
		    _sokoBanOptions.Add(new Dropdown.OptionData(level.name));
	    }

	    _gridWorldOptions = new List<Dropdown.OptionData>();
	    foreach (var level in GridWorldLevels)
	    {
		    _gridWorldOptions.Add(new Dropdown.OptionData(level.name));
	    }
	    
	    List<Dropdown.OptionData> agentOptions = new List<Dropdown.OptionData>();
	    foreach(var algorithm in Enum.GetValues(typeof(Algorithms)))
	    {
		    agentOptions.Add(new Dropdown.OptionData(algorithm.ToString()));
	    }

	    AgentDropDown.options = agentOptions;
    }

    private void Update()
    {
	    if (GameDropDown.value == 0)
	    {
		    LevelDropDown.options = _gridWorldOptions;
	    }
	    else if (GameDropDown.value == 1)
	    {
		    LevelDropDown.options = _sokoBanOptions;
	    }
    }

    public void OnPlay()
    {
	    if (GameDropDown.value == 0)
	    {
		    GridWorldGameManager._levelPreset = GridWorldLevels[LevelDropDown.value];
		    GridWorldGameManager.agentIndex = AgentDropDown.value;
		    SceneManager.LoadScene(1);
	    }
	    if (GameDropDown.value == 1)
	    {
		    SokobanGameManager.LevelPreset = SokobanLevels[LevelDropDown.value];
		    SokobanGameManager.agentIndex = AgentDropDown.value;
		    SceneManager.LoadScene(2);
	    }

	}
}
