using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData gameData;
    public int[] score;
    public float timeLimit;

    public static GameManager instance;

    public void Awake()
    {
        score = new int[gameData.numberOfPlayers];
        timeLimit = gameData.timeLimit;

        if (instance != null)
        {
            Debug.LogError("Duplicate Game Managers Found.");
            Destroy(gameObject);
        }
        instance = this;
    }

    public void Start()
    {
        
    }

    public void Update()
    {
        TimerTick();   
    }

    private void TimerTick()
    {
        timeLimit -= Time.deltaTime;

        if (timeLimit <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {

    }

    public void BeamItem()
    {

    }
}
