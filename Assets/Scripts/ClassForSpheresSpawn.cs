using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ClassForSpheresSpawn : MonoBehaviour
{
    [SerializeField] private Transform listOfSpotsForSphereSpawn;
    [SerializeField] private List<Sprite> listOfSpritesForSpheres;
    [SerializeField] private List<Sprite> listOfSpritesForEllipces;

    private void GetRandomSpheres(int numberOfSpheresToSpawn, int numberOfIndividualObjects, Action<int, int> addictionalAction = null)
    {
        List<int> idOfSpheresToRemove = new List<int>();

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

    private List<GameObject> MethodForSpawningSpheresInSpots(int numberOfSpheresToSpawn)
    {
        for (int i = 0; i < listOfSpotsForSphereSpawn.childCount; i++)
        {
            listOfSpotsForSphereSpawn.GetChild(i).gameObject.SetActive(false);
        }

        List<GameObject> sphereSpotsToSpawn = new List<GameObject>();

        GetRandomSpheres(numberOfSpheresToSpawn, listOfSpotsForSphereSpawn.childCount, (int id, int i) => 
        {
            sphereSpotsToSpawn.Add(listOfSpotsForSphereSpawn.GetChild(id).gameObject);
        });

        return sphereSpotsToSpawn;
    }

    public void MethodForSettingColorsOnSpawnedSpheres(int numberOfSpheresToSpawn)
    {
        List<GameObject> sphereSpotsToSpawn = MethodForSpawningSpheresInSpots(numberOfSpheresToSpawn);

        GetRandomSpheres(numberOfSpheresToSpawn, listOfSpritesForSpheres.Count, (int id, int i) =>
        {
            sphereSpotsToSpawn[i].GetComponent<Image>().sprite = listOfSpritesForSpheres[id];
            sphereSpotsToSpawn[i].transform.GetChild(0).GetComponent<Image>().sprite = listOfSpritesForEllipces[id];
        });

        foreach (GameObject spot in sphereSpotsToSpawn)
        {
            spot.SetActive(true);
        }
    }
}
