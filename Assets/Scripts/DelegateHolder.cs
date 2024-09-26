using System;
using UnityEngine;

public class DelegateHolder: MonoBehaviour 
{
    public delegate void NewDelegate();
    
    public NewDelegate SetPatternMenu, SetPatternGame, OnPauseGame, OnExitGame;
    public Action<int, int, float> OnGameStart;
    public Action<LevelDifficulty> SetDifficulty;
    public Action<int> DestroySpheresByMistake, NextLevel;
    public Action<int, int> OnWinLevel;
    public Action<int, bool> SpawnSpheres;
    public Action<float, float, float, float, bool> OnMakePositions;

    public void InvokeSetPatternMenu()
    {
        SetPatternMenu?.Invoke();
    }

    public void InvokeSetPatternGame()
    {
        SetPatternGame?.Invoke();
    }

    public void InvokeOnGameStart(int a, int b, float c)
    {
        OnGameStart?.Invoke(a, b, c);
    }

    public void InvokeSpawnSpheres(int num, bool color)
    {
        SpawnSpheres?.Invoke(num, color);
    }

    public void InvokeOnMakePositions(float down1, float up1, float down2, float up2, bool color)
    {
        OnMakePositions?.Invoke(down1, up1, down2, up2, color);
    }

    public void InvokeSetDifficulty(LevelDifficulty difficulty)
    {
        SetDifficulty?.Invoke(difficulty);
    }

    public void InvokeDestroySpheresByMistake(int newScore)
    {
        DestroySpheresByMistake?.Invoke(newScore);
    }

    public void InvokeWinLevel(int passedLevel, int score)
    {
        OnWinLevel?.Invoke(passedLevel, score);
    }

    public void InvokeNextLevel(int nextLevel)
    {
        NextLevel?.Invoke(nextLevel);
    }

    public void InvokeOnPauseGame()
    {
        OnPauseGame?.Invoke();
    }

    public void InvokeOnExitGame()
    {
        OnExitGame?.Invoke();
    }
}
