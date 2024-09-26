using System.Collections.Generic;
using UnityEngine;

public class ChooseLevelClass : MonoBehaviour
{
    public DelegateHolder delegateHolder;
    public GameObject instructions, inGame;
    public List<LevelDifficulty> levelDifficulty;
    public LevelDifficulty teachDifficulty;

    public void Start()
    {
        delegateHolder.NextLevel += ChooseLevel;
    }

    public void ChooseLevel(int levelNumber)
    {
        if (!PlayerPrefs.HasKey("Instructions") && levelNumber == 1)
        {
            delegateHolder.InvokeSetDifficulty(teachDifficulty);
            instructions.SetActive(true);
            PlayerPrefs.SetInt("Instructions", 0);
        }
        else if (transform.GetChild(1).GetChild(levelNumber - 1).GetChild(1).gameObject.activeSelf)
        {
            delegateHolder.InvokeSetDifficulty(levelDifficulty[levelNumber - 1]);
            delegateHolder.InvokeOnMakePositions(0.1f, 0.9f, 0.4f, 0.8f, true);
            delegateHolder.InvokeOnGameStart(levelNumber, levelDifficulty[levelNumber - 1].ScoreGoal, levelDifficulty[levelNumber - 1].SpeedOfSpheres);
        }
    }

    private void OnApplicationQuit()
    {
        delegateHolder.NextLevel -= ChooseLevel;
    }
}
