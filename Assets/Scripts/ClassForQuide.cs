using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClassForQuide : MonoBehaviour, IPointerClickHandler
{
    public ClassForSpawnControl spawnControl;
    public ClassForChangeColorControl changeColorControl;
    public GameObject bigPlate;
    public List<GameObject> quideTexts;

    private void OnEnable()
    {
        StartCoroutine(StartQuide());
    }

    IEnumerator StartQuide()
    {
        yield return new WaitForSecondsRealtime(2f);

        bigPlate.SetActive(true);
        quideTexts[0].SetActive(true);

        spawnControl.SpawnSpheres(spawnControl.infoAboutLevels.LevelsStructs[LevelsManager.varForSingleton.choosedLevelNumber].numberOfSpheresToSpawn);
        changeColorControl.SpawnSpheres(spawnControl.infoAboutLevels.LevelsStructs[LevelsManager.varForSingleton.choosedLevelNumber].numberOfSpheresToSpawn, spawnControl.sphereSpotsToSpawn);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Transform clickedSphere = eventData.pointerCurrentRaycast.gameObject.transform;
        if (clickedSphere.CompareTag("Sphere"))
        {
            if (clickedSphere.GetComponent<Image>().sprite == changeColorControl.targetImage.sprite && quideTexts[0].activeSelf)
            {
                clickedSphere.GetChild(0).gameObject.SetActive(true);

                quideTexts[0].SetActive(false);
                quideTexts[1].SetActive(true);

                StartCoroutine(DelayedRespawn(clickedSphere.GetChild(0).gameObject));
            }
        }
    }

    IEnumerator DelayedRespawn(GameObject sphereEllipce)
    {
        yield return new WaitForSecondsRealtime(3f);

        quideTexts[1].SetActive(false);
        quideTexts[2].SetActive(true);

        if (sphereEllipce != null)
        {
            sphereEllipce.SetActive(false);
        }

        changeColorControl.SpawnSpheres(spawnControl.infoAboutLevels.LevelsStructs[LevelsManager.varForSingleton.choosedLevelNumber].numberOfSpheresToSpawn, spawnControl.sphereSpotsToSpawn);

        PlayerPrefs.SetInt("Progress", 0);

        yield return new WaitForSecondsRealtime(3f);

        LevelsManager.varForSingleton.ChooseNewLevel(1);
        gameObject.SetActive(false);
    }

}
