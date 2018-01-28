using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour {

    public Toggle[] toggles;

    public GameData gameData;

    public void SetToggles()
    {
        for (int i = 0; i < 4; i++)
        {
            toggles[i].isOn = gameData.playerYAxisInverted[i];
        }
    }
}
