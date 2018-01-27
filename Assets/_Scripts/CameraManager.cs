using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public GameData GameData;
    public GameManager gm;

    public Camera[] cameraArray;
    private int numberOfPlayers;

    public bool HorizontalMode;

	// Use this for initialization
    
	void Awake () {
        numberOfPlayers = GameData.numberOfPlayers;
        HorizontalMode = GameData.HorizontalMode;

        if(HorizontalMode == true)
        {
            
            if (numberOfPlayers == 2)
            {
                cameraArray[0].rect = new Rect(0f, .5f, 1f, .5f); //Top Half
                cameraArray[1].rect = new Rect(0f, 0f, 1f, .5f); //Bottom Half
                cameraArray[2].enabled = false;
                cameraArray[3].enabled = false;

            }
            else if (numberOfPlayers == 3)
            {
                cameraArray[0].rect = new Rect(0, .5f, .5f, .5f);
                cameraArray[1].rect = new Rect(.5f, .5f, .5f, .5f);
                cameraArray[2].rect = new Rect(0f, 0, 1f, .5f);
                cameraArray[3].enabled = false;
            }
            else if (numberOfPlayers == 4)
            {
                cameraArray[0].rect = new Rect(0f, .5f, .5f, .5f);
                cameraArray[1].rect = new Rect(.5f, .5f, .5f, .5f);
                cameraArray[2].rect = new Rect(0f, 0f, .5f, .5f);
                cameraArray[3].rect = new Rect(.5f, 0f, .5f, .5f);
            }
        }
        else
        {
            if (numberOfPlayers == 2)
            {

                cameraArray[0].rect = new Rect(0, 0, .5f, 1f);
                cameraArray[1].rect = new Rect(.5f, 0, .5f, 1);
                cameraArray[2].enabled = false;
                cameraArray[3].enabled = false;

            }
            else if (numberOfPlayers == 3)
            {
                cameraArray[0].rect = new Rect(0f, 0f, 1/3f, 1f);
                cameraArray[1].rect = new Rect(1/3f, 0f, 1/3f, 1f);
                cameraArray[2].rect = new Rect(2/3f, 0, 1/3f, 1f);
                cameraArray[3].enabled = false;
            }
            else if (numberOfPlayers == 4)
            {
                cameraArray[0].rect = new Rect(0f, .5f, .5f, .5f);
                cameraArray[1].rect = new Rect(.5f, .5f, .5f, .5f);
                cameraArray[2].rect = new Rect(0f, 0f, .5f, .5f);
                cameraArray[3].rect = new Rect(.5f, 0f, .5f, .5f);
            }
        }
    }

    private void Start()
    {
        gm = GameManager.instance;

        if(gm != null)
        {
            for(int i = 0; i < GameData.numberOfPlayers; i++)
            {
                //cameraArray[i].GetComponent<ShooterGameCamera>().target = ;
            }
        }
    }
    private void Update()
    {
        
    }
}
