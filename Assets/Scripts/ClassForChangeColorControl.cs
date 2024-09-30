using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassForChangeColorControl : BaseClassForSpawnedSpheresControl
{
    [SerializeField] private List<Sprite> listOfSpritesForSpheres;
    [SerializeField] private List<Sprite> listOfSpritesForEllipces;
    public Image targetImage;

    public override void MethodForSettingColorsOnSpawnedSpheres(int numberOfSpheresToSpawn, List<GameObject> sphereSpotsToSpawn)
    {
        GetRandomSpheres(numberOfSpheresToSpawn, listOfSpritesForSpheres.Count, (int id, int i) =>
        {
            sphereSpotsToSpawn[i].GetComponent<Image>().sprite = listOfSpritesForSpheres[id];
            sphereSpotsToSpawn[i].transform.GetChild(0).GetComponent<Image>().sprite = listOfSpritesForEllipces[id];
        });

        targetImage.sprite = sphereSpotsToSpawn[Random.Range(0, sphereSpotsToSpawn.Count)].GetComponent<Image>().sprite;
    }
}
