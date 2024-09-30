using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ClassForCountingPoints: MonoBehaviour
{
    public GameplayControl gameControl;
    public TextMeshProUGUI playerPoints, playerTime;
    public int playerPointsCount;

    public void ChangePlayerPoints(int points)
    {
        playerPointsCount += points;

        if(playerPointsCount < 0)
        {
            playerPointsCount = 0;
        }
        else if(playerPointsCount == gameControl.spawnControl.infoAboutLevels.LevelsStructs[LevelsManager.varForSingleton.choosedLevelNumber].goalPoints)
        {
            LevelsManager.varForSingleton.choosedLevelNumber += 1;
            MethodForWinGameEvent();
        }
        else if(playerPointsCount % 100 == 0)
        {
            MethodForBooster();
        }

        playerPoints.text = playerPointsCount.ToString();
    }

    public void MethodForWinGameEvent()
    {
        gameControl.enabled = false;
        gameControl.StopAllCoroutines();
        gameControl.winPanel.SetActive(true);
    }

    public void MethodForBooster()
    {
        float originalSpeed = gameControl.sphereSpeed;
        float originalDelayTime = gameControl.timeOfChange;

        gameControl.sphereSpeed /= 2;
        gameControl.timeOfChange *= 2;

        StartCoroutine(BoosterProcess(originalSpeed, originalDelayTime));
    }

    IEnumerator BoosterProcess(float originalSpeed, float originalDelayTime)
    {
        yield return new WaitForSecondsRealtime(5f);

        gameControl.sphereSpeed = originalSpeed;
        gameControl.timeOfChange = originalDelayTime;
    }
}
