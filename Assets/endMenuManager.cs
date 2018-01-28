using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endMenuManager : MonoBehaviour {


    public GameData gameData;
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject playerThree;
    public GameObject playerFour;
    private GameObject[] playerList = new GameObject[4];

    public Text scoreOne;
    public Text scoreTwo;
    public Text scoreThree;
    public Text scoreFour;
    private Text[] scoreTextList = new Text[4];

    // Use this for initialization
    void Start () {
        playerList[0] = playerOne;
        playerList[1] = playerTwo;
        playerList[2] = playerThree;
        playerList[3] = playerFour;
        scoreTextList[0] = scoreOne;
        scoreTextList[1] = scoreTwo;
        scoreTextList[2] = scoreThree;
        scoreTextList[3] = scoreFour;

        playerOne.SetActive(false);
        playerTwo.SetActive(false);
        playerThree.SetActive(false);
        playerFour.SetActive(false);

        for(int count = 0; count < gameData.numberOfPlayers; count++)
        {
            playerList[count].SetActive(true);
            scoreTextList[count].text = gameData.playerScores[count].ToString();
        }
	}

    public void exitPressed()
    {
        Application.Quit();
    }
	
}
