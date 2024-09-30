using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameplayControl : MonoBehaviour, IPointerClickHandler
{
    public ClassForSpawnControl spawnControl;
    public ClassForChangeColorControl changeColorControl;
    public ClassForCountingPoints varForCountingPoints;
    public Animator gameAnimator;
    public GameObject winPanel;
    public float timeOfChange, timeInGame, sphereSpeed;
    public bool gameplayComing;

    public void Update()
    {
        float dicreaseForSmoothMovingOnX = spawnControl.listOfSpotsForSphereSpawn.position.x;
        float dicreaseForSmoothMovingOnY = spawnControl.listOfSpotsForSphereSpawn.position.y;

        if (spawnControl.infoAboutLevels.LevelsStructs[LevelsManager.varForSingleton.choosedLevelNumber].verticalMoving)
        {
            dicreaseForSmoothMovingOnY -= 0.1f;
        }
        else if (!spawnControl.infoAboutLevels.LevelsStructs[LevelsManager.varForSingleton.choosedLevelNumber].verticalMoving)
        {
            dicreaseForSmoothMovingOnX -= 0.1f;
        }

        if (gameplayComing)
        {
            Vector2 newPosition = new(dicreaseForSmoothMovingOnX, dicreaseForSmoothMovingOnY);
            spawnControl.listOfSpotsForSphereSpawn.position = Vector2.Lerp(spawnControl.listOfSpotsForSphereSpawn.position, newPosition, sphereSpeed * Time.deltaTime);
        }
    }

    public void FixedUpdate()
    {
        if (gameplayComing)
        {
            timeInGame += Time.deltaTime;
        }
    }

    public void ResetStats()
    {
        sphereSpeed = spawnControl.infoAboutLevels.LevelsStructs[LevelsManager.varForSingleton.choosedLevelNumber].speedOfSpawnedSpheres;
        timeInGame = 0;
        varForCountingPoints.playerPointsCount = 0;
    }

    public void MethodForWinGameEvent()
    {
        StopAllCoroutines();
        gameplayComing = false;
        winPanel.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Transform clickedSphere = eventData.pointerCurrentRaycast.gameObject.transform;
        if (clickedSphere.CompareTag("Sphere") && gameplayComing)
        {
            gameplayComing = false;

            if (clickedSphere.GetComponent<Image>().sprite == changeColorControl.targetImage.sprite)
            {
                clickedSphere.GetChild(0).gameObject.SetActive(true);
                varForCountingPoints.ChangePlayerPoints(5);
            }
            else
            {
                varForCountingPoints.ChangePlayerPoints(-15);
            }

            gameAnimator.SetTrigger("FlyOut");
            StartCoroutine(DelayedRespawn(clickedSphere.GetChild(0).gameObject));
        }
    }

    public void MethodForSpawningNewSpheres()
    {
        spawnControl.MethodForSpawningSpheresInSpots(spawnControl.infoAboutLevels.LevelsStructs[LevelsManager.varForSingleton.choosedLevelNumber].numberOfSpheresToSpawn);
        changeColorControl.MethodForSettingColorsOnSpawnedSpheres(spawnControl.infoAboutLevels.LevelsStructs[LevelsManager.varForSingleton.choosedLevelNumber].numberOfSpheresToSpawn, spawnControl.sphereSpotsToSpawn);
        gameplayComing = true;
        StartCoroutine(ChangeColorOfSpawnedSpheres());
    }

    public IEnumerator DelayedRespawn(GameObject sphereEllipce)
    {
        yield return new WaitForSecondsRealtime(1f);

        spawnControl.listOfSpotsForSphereSpawn.localPosition = spawnControl.startPosition;
        if (sphereEllipce != null)
        {
            sphereEllipce.SetActive(false);
        }

        gameAnimator.SetTrigger("FlyIn");

        StopAllCoroutines();
        MethodForSpawningNewSpheres();
    }

    IEnumerator ChangeColorOfSpawnedSpheres()
    {
        while(gameplayComing)
        {
            yield return new WaitForSecondsRealtime(timeOfChange);

            changeColorControl.MethodForSettingColorsOnSpawnedSpheres(spawnControl.infoAboutLevels.LevelsStructs[LevelsManager.varForSingleton.choosedLevelNumber].numberOfSpheresToSpawn, spawnControl.sphereSpotsToSpawn);
        }
    }
}
