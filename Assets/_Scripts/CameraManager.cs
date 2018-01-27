using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public GameData GameData;
    public Camera Camera1;
    public Camera Camera2;
    public Camera Camera3;
    public Camera Camera4;

    private int numberOfPlayers;


	// Use this for initialization
	void Start () {
        numberOfPlayers = GameData.numberOfPlayers;

        if (numberOfPlayers == 2)
        {
            Camera1.rect = new Rect(0, 0, .5f, 1);
            Camera2.rect = new Rect(.5f, 0, .5f, 1);
        }
        else if (numberOfPlayers == 3)
        {
            Camera1.rect = new Rect(0f, .5f, .5f, .5f);
            Camera2.rect = new Rect(.5f, .5f, .5f, .5f);
            Camera3.rect = new Rect(.25f, 0f, .5f, .5f);
        }
        else if (numberOfPlayers == 4)
        {
            Camera1.rect = new Rect(0f, .5f, .5f, .5f);
            Camera2.rect = new Rect(.5f, .5f, .5f, .5f);
            Camera3.rect = new Rect(0f, 0f, .5f, .5f);
            Camera4.rect = new Rect(.5f, 0f, .5f, .5f);
        }
    }

    private void Update()
    {
        
    }
}
