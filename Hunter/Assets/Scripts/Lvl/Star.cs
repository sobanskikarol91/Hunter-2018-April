﻿using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Star
{
    public Image starImg;
    public int scoreToGainStar;
    public bool Gained { get; private set; }

    public bool CheckIfPlayerGainedStar(float playerScore)
    {
        // TODO: Change img
        return playerScore >= scoreToGainStar;
    }
}
