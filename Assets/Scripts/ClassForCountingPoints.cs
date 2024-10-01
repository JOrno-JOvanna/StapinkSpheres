using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ClassForCountingPoints: MonoBehaviour
{
    public InitialProgress progress;
    public GameplayControl gameControl;
    public TextMeshProUGUI playerPoints, playerTime;
    public GameObject boosterPanel, prizePanel;
    public UnityEngine.UI.Image prizeIcon;
    public List<Sprite> prizes;
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
            if(new[] { 3, 6, 9, 12 }.Contains(LevelsManager.varForSingleton.choosedLevelNumber))
            {
                prizeIcon.sprite = prizes[LevelsManager.varForSingleton.choosedLevelNumber / 3 - 1];
                prizePanel.SetActive(true);
                gameControl.gameAnimator.SetTrigger("Prize");
                StartCoroutine(WinProcess());
            }
            else
            {
                MethodForWinGameEvent();
            }

        }
        else if(playerPointsCount % 100 == 0)
        {
            boosterPanel.SetActive(true);
            gameControl.gameAnimator.SetTrigger("Prize");
            MethodForBooster();
        }

        playerPoints.text = playerPointsCount.ToString();
    }

    public void MethodForWinGameEvent()
    {
        PlayerPrefs.SetInt("Progress", LevelsManager.varForSingleton.choosedLevelNumber + 1);
        progress.CheckProgress();
        LevelsManager.varForSingleton.choosedLevelNumber += 1;
        playerPointsCount = 0;

        gameControl.enabled = false;
        gameControl.StopAllCoroutines();
        gameControl.winPanel.SetActive(true);
    }

    public void MethodForBooster()
    {
        float originalSpeed = gameControl.sphereSpeed;
        float originalDelayTime = gameControl.timeOfChange;

        gameControl.sphereSpeed /= 3;
        gameControl.timeOfChange *= 2;

        StartCoroutine(BoosterProcess(originalSpeed, originalDelayTime));
    }

    IEnumerator BoosterProcess(float originalSpeed, float originalDelayTime)
    {
        yield return new WaitForSecondsRealtime(1f);
        boosterPanel.SetActive(false);

        yield return new WaitForSecondsRealtime(5f);

        boosterPanel.SetActive(false);

        gameControl.sphereSpeed = originalSpeed;
        gameControl.timeOfChange = originalDelayTime;
    }

    IEnumerator WinProcess()
    {
        yield return new WaitForSecondsRealtime(1f);

        prizePanel.SetActive(false);
        MethodForWinGameEvent();
    }
}
