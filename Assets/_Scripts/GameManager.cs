using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData gameData;

    public GameEvent onPlayerScored;

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
    }

    public void Start()
    {
        
    }

    public void Update()
    {
        TimerTick();   
    }
    #endregion

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
        }
    }

    private void EndGame()
    {

    }

    public void AddItemToPlayer(int playerID, ItemData data)
    {
        playerItems[playerID].Add(data);
        playerScores[0] += data.pointValue;
        onPlayerScored.Raise();
    }
}
