using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData gameData;

    public GameEvent onPlayerScored;

    public GameObject[] players;
    public Transform[] playerSpawns;


    private bool gameRunning = true;
    private int[] playerScores;
    private float timeLimit;
    private List<ItemData>[] playerItems;
    

    public static GameManager instance;

    #region Monobehaviours
    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Duplicate Game Managers Found.");
            Destroy(gameObject);
        }
        instance = this;

        InstantiateManager();
        SpawnPlayers();
    }

    public void Start()
    {

    }

    public void Update()
    {
        if (gameRunning)
            TimerTick();
    }
    #endregion

    private void SpawnPlayers()
    {
        for (int i = gameData.numberOfPlayers-1; i < players.Length; i++)
        {
            players[i].SetActive(false);
            players[i].transform.position = playerSpawns[i].position;
        }
    }

    private void InstantiateManager()
    {
        // Create arrays
        playerScores = new int[gameData.numberOfPlayers];
        playerItems = new List<ItemData>[gameData.numberOfPlayers];

        // Fill up arrays
        for (int i = 0; i < gameData.numberOfPlayers; i++)
        {
            playerItems[i] = new List<ItemData>();
            playerScores[i] = 0;
        }

        // Starting variable values
        timeLimit = gameData.timeLimit;
    }

    private void TimerTick()
    {
        timeLimit -= Time.deltaTime;

        if (timeLimit <= 0)
        {
            EndGame();
            timeLimit = 0;
        }
    }

    private void EndGame()
    {
        gameRunning = false;
    }

    public void AddItemToPlayer(int playerID, ItemData data)
    {
        playerItems[playerID].Add(data);
        playerScores[playerID] += data.pointValue;
        onPlayerScored.Raise();
    }

    public int GetPlayerScore(int playerID)
    {
        return playerScores[playerID];
    }

    public float GetTimeLeft()
    {
        return timeLimit;
    }
}
