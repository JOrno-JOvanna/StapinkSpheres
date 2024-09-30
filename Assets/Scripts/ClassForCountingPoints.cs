using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClassForCountingPoints
{
    public int playerPoints;

    public void ChangePlayerPoints(int points)
    {
        playerPoints += points;

        if(playerPoints < 0)
        {
            playerPoints = 0;
        }
        else if(playerPoints % 100 == 0)
        {
            Debug.Log("booster activated");
        }
    }
}
