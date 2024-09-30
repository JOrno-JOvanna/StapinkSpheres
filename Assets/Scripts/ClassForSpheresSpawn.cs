using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ClassForSpheresSpawn : MonoBehaviour
{
    [SerializeField] private protected ClassForInformationAboutLevel infoAboutLevels;
    [SerializeField] private protected Animator gameAnimator;
    [SerializeField] private protected Transform listOfSpotsForSphereSpawn;
    [SerializeField] private List<Sprite> listOfSpritesForSpheres;
    [SerializeField] private List<Sprite> listOfSpritesForEllipces;
    [SerializeField] private protected Image targetImage;
    private List<GameObject> sphereSpotsToSpawn = new();
    private List<GameObject> originalPositionsOfSpotsToSpawn = new();
    private protected Vector2 startPosition;
    private protected float sphereSpeed;

    private void GetRandomSpheres(int numberOfSpheresToSpawn, int numberOfIndividualObjects, Action<int, int> addictionalAction = null)
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

    private protected void MethodForSpawningSpheresInSpots(int numberOfSpheresToSpawn)
    {
        sphereSpeed = infoAboutLevels.LevelsStructs[LevelsManager.varForSingleton.choosedLevelNumber].speedOfSpawnedSpheres;
        startPosition = listOfSpotsForSphereSpawn.position;

        for (int i = 0; i < listOfSpotsForSphereSpawn.childCount; i++)
        {
            listOfSpotsForSphereSpawn.GetChild(i).gameObject.SetActive(false);
        }

        if (sphereSpotsToSpawn.Count != 0)
        {
            for (int i = 0; i < sphereSpotsToSpawn.Count; i++)
            {
                sphereSpotsToSpawn[i].transform.position = originalPositionsOfSpotsToSpawn[i].transform.position;
            }

            sphereSpotsToSpawn.Clear();
            originalPositionsOfSpotsToSpawn.Clear();
        }

        //for (int i = 0; i < listOfSpotsForSphereSpawn.childCount; i++)
        //{
        //    listOfSpotsForSphereSpawn.GetChild(i).gameObject.SetActive(false);
        //}

        List<GameObject> newSphereSpotsToSpawn = new();

        GetRandomSpheres(numberOfSpheresToSpawn, listOfSpotsForSphereSpawn.childCount, (int id, int i) => 
        {
            newSphereSpotsToSpawn.Add(listOfSpotsForSphereSpawn.GetChild(id).gameObject);
        });

        sphereSpotsToSpawn = newSphereSpotsToSpawn;
        originalPositionsOfSpotsToSpawn = newSphereSpotsToSpawn;

        MethodForSettingColorsOnSpawnedSpheres(numberOfSpheresToSpawn);

        foreach (GameObject spot in sphereSpotsToSpawn)
        {
            spot.SetActive(true);
        }

        gameAnimator.SetTrigger("FlyIn");
    }

    private protected void MethodForSettingColorsOnSpawnedSpheres(int numberOfSpheresToSpawn)
    {
        GetRandomSpheres(numberOfSpheresToSpawn, listOfSpritesForSpheres.Count, (int id, int i) =>
        {
            sphereSpotsToSpawn[i].GetComponent<Image>().sprite = listOfSpritesForSpheres[id];
            sphereSpotsToSpawn[i].transform.GetChild(0).GetComponent<Image>().sprite = listOfSpritesForEllipces[id];
        });

        targetImage.sprite = sphereSpotsToSpawn[UnityEngine.Random.Range(0, sphereSpotsToSpawn.Count)].GetComponent<Image>().sprite;
    }
}
