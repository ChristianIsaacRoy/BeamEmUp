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
    public Text scoreOne;
    public Text scoreTwo;
    public Text scoreThree;
    public Text scoreFour; 

	// Use this for initialization
	void Start () {
		
	}

    public void exitPressed()
    {
        Application.Quit();
    }
	
}
