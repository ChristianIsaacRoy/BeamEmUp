﻿using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public int numberOfPlayers = 2;
    public int timeLimit = 30;
    public float distanceToZap = 6.0f;
    public float timeToZap = 1.5f;

    public bool HorizontalMode;
}
