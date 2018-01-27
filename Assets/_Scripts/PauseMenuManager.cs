using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}

    public void resumePressed ()
    {

    }

    public void exitPressed()
    {
        Application.Quit();
    }

    public void menuPressed() {
        SceneManager.LoadScene("Start Menu");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
