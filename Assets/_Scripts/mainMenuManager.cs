﻿using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Rewired;

public class MainMenuManager : MonoBehaviour
{
    public GameData gameData;

    public GameObject startMenu;
    public GameObject playerCountChoiceMenu;
    public GameObject settingsMenu;

    public GameObject defaultStartMenuGO;
    public GameObject defaultPlayerCountGO;
    public GameObject defaultSettingsMenuGO;

    public Player firstPlayer;

    public void Awake()
    {
        firstPlayer = ReInput.players.GetPlayer(0);
    }

    public void Start()
    {
        OpenMainMenu();
    }

    public void Update()
    {
        if (firstPlayer.GetButtonDown("UICancel") && playerCountChoiceMenu.activeSelf)
        {
            OpenMainMenu();
        }
    }

    public void OpenMainMenu()
    {
        startMenu.SetActive(true);
        playerCountChoiceMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(defaultStartMenuGO);
    }

    public void OpenSettingsMenu()
    {
        startMenu.SetActive(false);
        settingsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(defaultSettingsMenuGO);
    }

    public void OpenCharCountMenu()
    {
        startMenu.SetActive(false);
        playerCountChoiceMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(defaultPlayerCountGO);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame(int mode)
    {
        //Modes 1-4 correlate to 1-4 player games
        gameData.numberOfPlayers = mode;
        SceneManager.LoadSceneAsync("ChrisTest");
    }
    
}
