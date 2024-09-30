using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager varForSingleton;
    public GameObject mainGameCanvas;
    public int choosedLevelNumber;

    public void Awake()
    {
        if (varForSingleton == null)
        {
            varForSingleton = this;
        }
    }

    public void ChooseNewLevel(int levelNumber)
    {
        choosedLevelNumber = levelNumber;

        mainGameCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
