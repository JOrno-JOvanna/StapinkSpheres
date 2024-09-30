using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseClassForSpawnedSpheresControl : MonoBehaviour
{
    public void GetRandomSpheres(int numberOfSpheresToSpawn, int numberOfIndividualObjects, Action<int, int> addictionalAction = null)
    {
        List<int> idOfSpheresToRemove = new();

        for (int i = 0; i < numberOfIndividualObjects; i++)
        {
            idOfSpheresToRemove.Add(i);
        }

        for (int i = 0; i < numberOfSpheresToSpawn; i++)
        {
            int id = UnityEngine.Random.Range(0, idOfSpheresToRemove.Count);
            addictionalAction?.Invoke(idOfSpheresToRemove[id], i);
            idOfSpheresToRemove.RemoveAt(id);
        }
    }

    public virtual void MethodForSettingColorsOnSpawnedSpheres(int numberOfSpheresToSpawn, List<GameObject> sphereSpotsToSpawn)
    {

    }

    public virtual void MethodForSpawningSpheresInSpots(int numberOfSpheresToSpawn)
    {

    }
}
