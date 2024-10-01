using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager varForSingleton;
    public GameplayControl gameCon;
    public GameObject quidePanel;
    public int choosedLevelNumber;

    public void Awake()
    {
        PlayerPrefs.DeleteAll();

        if (varForSingleton == null)
        {
            varForSingleton = this;
        }
    }

    public void ChooseNewLevel(int levelNumber)
    {
        if(!PlayerPrefs.HasKey("Progress"))
        {
            choosedLevelNumber = 0;
            quidePanel.SetActive(true);
        }
        else
        {
            choosedLevelNumber = levelNumber;
            gameCon.ResetStats();
            gameCon.enabled = true;
            gameCon.MethodForSpawningNewSpheres();
        }

        gameObject.SetActive(false);
    }
}
