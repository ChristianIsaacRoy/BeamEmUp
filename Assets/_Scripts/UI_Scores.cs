using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scores : MonoBehaviour
{
    private GameManager gm;

    public Text p1Text;
    public Text p2Text;
    public Text p3Text;
    public Text p4Text;

    public void Start()
    {
        gm = GameManager.instance;   
    }

    public void UpdateText()
    {
        if (gm != null)
        {
            p1Text.text = gm.GetPlayerScore(0).ToString();
            p2Text.text = gm.GetPlayerScore(1).ToString();
            p3Text.text = gm.GetPlayerScore(2).ToString();
            p4Text.text = gm.GetPlayerScore(3).ToString();
        }
        else
        {
            p1Text.text = "0";
            p2Text.text = "0";
            p3Text.text = "0";
            p4Text.text = "0";
        }
    }
}
