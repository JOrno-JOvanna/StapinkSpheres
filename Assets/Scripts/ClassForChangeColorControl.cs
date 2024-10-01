using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassForChangeColorControl : BaseClassForSpawnedSpheresControl
{
    [SerializeField] private List<Sprite> listOfSpritesForSpheres;
    [SerializeField] private List<Sprite> listOfSpritesForEllipces;
    public Image targetImage;

    protected override void MethodForSettingColorsOnSpawnedSpheres(int numberOfSpheresToSpawn, List<GameObject> sphereSpotsToSpawn)
    {
        GetRandomSpheres(numberOfSpheresToSpawn, listOfSpritesForSpheres.Count, (int id, int i) =>
        {
            var image = sphereSpotsToSpawn[i].GetComponent<Image>();
            image.sprite = listOfSpritesForSpheres[id];
            image.transform.GetChild(0).GetComponent<Image>().sprite = listOfSpritesForEllipces[id];
        });

        targetImage.sprite = sphereSpotsToSpawn[Random.Range(0, sphereSpotsToSpawn.Count)].GetComponent<Image>().sprite;
    }
}
