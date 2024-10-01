using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClassForSpawnControl : BaseClassForSpawnedSpheresControl
{
    public ClassForInformationAboutLevel infoAboutLevels;
    [SerializeField] private Animator gameAnimator;
    public Transform listOfSpotsForSphereSpawn;
    public Vector2 startPosition;
    public List<GameObject> sphereSpotsToSpawn;

    private List<GameObject> allSpots = new();

    private void Awake()
    {
        CacheSpots();
    }

    private void CacheSpots()
    {
        allSpots.Clear();
        for (int i = 0; i < listOfSpotsForSphereSpawn.childCount; i++)
        {
            allSpots.Add(listOfSpotsForSphereSpawn.GetChild(i).gameObject);
        }
    }

    protected override void MethodForSpawningSpheresInSpots(int numberOfSpheresToSpawn)
    {
        startPosition = listOfSpotsForSphereSpawn.localPosition;

        foreach (var spot in allSpots)
        {
            spot.SetActive(false);
        }

        sphereSpotsToSpawn.Clear();

        GetRandomSpheres(numberOfSpheresToSpawn, allSpots.Count, (int id, int i) =>
        {
            sphereSpotsToSpawn.Add(allSpots[id]);
        });

        foreach (GameObject spot in sphereSpotsToSpawn)
        {
            spot.SetActive(true);
        }
    }
}
