using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager varForSingleton;
    public GameplayControl gameCon;
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

        gameCon.enabled = true;
        gameCon.MethodForSpawningNewSpheres();
        gameObject.SetActive(false);
    }
}
