using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData gameData;

    public GameEvent onPlayerScored;

    public GameObject playerPrefab;

    private bool gameRunning = true;
    private int[] playerScores;
    private float timeLimit;
    private List<ItemData>[] playerItems;
    private GameObject[] players;

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
        CreatePlayers();
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

    private void CreatePlayers()
    {
        for (int i = 0; i < gameData.numberOfPlayers; i++)
        {
            players[i] = Instantiate(playerPrefab);
            players[i].GetComponent<PlayerController>().playerID = i;
        }
    }

    private void InstantiateManager()
    {
        // Create arrays
        playerScores = new int[gameData.numberOfPlayers];
        playerItems = new List<ItemData>[gameData.numberOfPlayers];
        players = new GameObject[gameData.numberOfPlayers];

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
