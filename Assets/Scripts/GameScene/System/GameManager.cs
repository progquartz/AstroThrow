using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool IsGameOver {  get; private set; }



    public void Init()
    {

    }


    private void Update()
    {
        if(IsGameOver)
        {
            Debug.Log("°×¿À¹ö!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
    }

}
