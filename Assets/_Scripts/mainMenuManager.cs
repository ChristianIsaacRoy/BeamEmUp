using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuManager : MonoBehaviour {

    public GameObject startMenu;
    public GameObject PlayerCountChoiceMenu;

    // Use this for initialization
    void Start () {
        startMenu.SetActive(true);
        PlayerCountChoiceMenu.SetActive(false);
    }

    public void startPressed ()
    {
        startMenu.SetActive(false);
        PlayerCountChoiceMenu.SetActive(true);
    }
	
    public void exitPressed ()
    {
        Application.Quit();
    }

    public void startGame (int Mode)
    {
        //Modes 1-4 correlate to 1-4 player games

    }

	// Update is called once per frame
	void Update () {
		
	}
}
