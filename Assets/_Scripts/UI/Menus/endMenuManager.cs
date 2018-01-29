using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endMenuManager : MonoBehaviour {

    public GameObject defaultSelectedObject;

    public GameData gameData;
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject playerThree;
    public GameObject playerFour;

    public Image fadeImage;

    private GameObject[] playerList = new GameObject[4];

    public Text scoreOne;
    public Text scoreTwo;
    public Text scoreThree;
    public Text scoreFour;
    private Text[] scoreTextList = new Text[4];

    private Text highScore;

    // Use this for initialization
    void Start () {
        EventSystem.current.SetSelectedGameObject(defaultSelectedObject);

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

        highScore = scoreOne;
        int highScoreNum = gameData.playerScores[0];
        GameObject winner = playerOne;

        for (int count = 0; count < gameData.numberOfPlayers; count++)
        {
            playerList[count].SetActive(true);
            scoreTextList[count].text = gameData.playerScores[count].ToString();
            //find winner
            if(gameData.playerScores[count] > highScoreNum)
            {
                highScore = scoreTextList[count];
                highScoreNum = gameData.playerScores[count];
                winner = playerList[count];
            }
        }
        highScore.fontSize = 60;
        highScore.GetComponent<Text>().color = Color.green;
        winner.transform.Translate(0, 0, 2.5f);
        winner.transform.Rotate(new Vector3(-19, 0, 0));

        winner.transform.Find("AlienPlayer").GetComponent<Animator>().SetBool("isGrounded", false);
    }

    public void Awake()
    {
        fadeImage.GetComponent<Animator>().SetTrigger("fadeIn");
    }

    public void PlayAgain()
    {
        SceneManager.LoadSceneAsync("WhiteboxEditing");
    }

    public void exitPressed()
    {
        SceneManager.LoadSceneAsync("Start Menu");
    }
	

}
