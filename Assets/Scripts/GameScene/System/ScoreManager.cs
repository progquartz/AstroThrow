using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int Score;

    public void Init()
    {

    }

    public void AddScore(int amount)
    {
        Score += amount;
    }

    public void ResetScore()
    {
        Score = 0;
    }

    public void UpdateScore()
    {
        int scoreUpdated = ProjectileManager.instance.GetTotalScore();
        Score = scoreUpdated;
    }

}
