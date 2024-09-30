using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassForPauseMenu : MonoBehaviour
{
    public GameplayControl control;

    public void MethodForRestartLevel()
    {
        control.StopAllCoroutines();
        control.gameAnimator.SetTrigger("FlyOut");
        control.ResetStats();
        control.StartCoroutine(control.DelayedRespawn(null));
    }

    public void StopGamePlay(bool newGameStatement)
    {
        control.StopAllCoroutines();
        control.gameplayComing = newGameStatement;
    }

    public void ExitGame()
    {
        control.spawnControl.listOfSpotsForSphereSpawn.localPosition = control.spawnControl.startPosition;
    }
}
