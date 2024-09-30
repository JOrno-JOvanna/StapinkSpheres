using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassForSpawnControl : BaseClassForSpawnedSpheresControl
{
    public ClassForInformationAboutLevel infoAboutLevels;
    [SerializeField] private Animator gameAnimator;
    public Transform listOfSpotsForSphereSpawn;
    public Vector2 startPosition;
    public List<GameObject> sphereSpotsToSpawn;

    public override void MethodForSpawningSpheresInSpots(int numberOfSpheresToSpawn)
    {
        startPosition = listOfSpotsForSphereSpawn.localPosition;

        for (int i = 0; i < listOfSpotsForSphereSpawn.childCount; i++)
        {
            listOfSpotsForSphereSpawn.GetChild(i).gameObject.SetActive(false);
        }

        if (sphereSpotsToSpawn.Count != 0)
        {
            sphereSpotsToSpawn.Clear();
        }

        List<GameObject> newSphereSpotsToSpawn = new();

        GetRandomSpheres(numberOfSpheresToSpawn, listOfSpotsForSphereSpawn.childCount, (int id, int i) =>
        {
            newSphereSpotsToSpawn.Add(listOfSpotsForSphereSpawn.GetChild(id).gameObject);
        });

        sphereSpotsToSpawn = newSphereSpotsToSpawn;

        foreach (GameObject spot in sphereSpotsToSpawn)
        {
            spot.SetActive(true);
        }
    }
}
