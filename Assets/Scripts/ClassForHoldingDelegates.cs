using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClassForHoldingDelegates : MonoBehaviour
{
    public delegate void NewChangePointsDelegate(int a);
    public NewChangePointsDelegate AddPointsForPlayer;
    public NewChangePointsDelegate AddCountForBooster;

    public void OnPointsChange(int a)
    {
        AddPointsForPlayer?.Invoke(a);
    }
}
