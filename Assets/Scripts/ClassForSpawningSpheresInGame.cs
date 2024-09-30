using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClassForSpawningSpheresInGame : ClassForSpheresSpawn, IPointerClickHandler
{
    public ClassForCountingPoints varForCountingPoints;
    [SerializeField] private bool gameplayComing;

    public void OnEnable()
    {
        MethodForSpawningNewSpheres();
    }

    public void Update()
    {
        float dicreaseForSmoothMovingOnX = listOfSpotsForSphereSpawn.position.x;
        float dicreaseForSmoothMovingOnY = listOfSpotsForSphereSpawn.position.y;

        if (infoAboutLevels.LevelsStructs[LevelsManager.varForSingleton.choosedLevelNumber].verticalMoving)
        {
            dicreaseForSmoothMovingOnY -= 0.1f;
        }
        else if (!infoAboutLevels.LevelsStructs[LevelsManager.varForSingleton.choosedLevelNumber].verticalMoving)
        {
            dicreaseForSmoothMovingOnX -= 0.1f;
        }

        if (gameplayComing)
        {
            Vector2 newPosition = new(dicreaseForSmoothMovingOnX, dicreaseForSmoothMovingOnY);
            listOfSpotsForSphereSpawn.position = Vector2.Lerp(listOfSpotsForSphereSpawn.position, newPosition, sphereSpeed * Time.deltaTime);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Transform clickedSphere = eventData.pointerCurrentRaycast.gameObject.transform;
        if (clickedSphere.CompareTag("Sphere") && gameplayComing)
        {
            gameplayComing = false;

            if (clickedSphere.GetComponent<Image>().sprite == targetImage.sprite)
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
        MethodForSpawningSpheresInSpots(infoAboutLevels.LevelsStructs[LevelsManager.varForSingleton.choosedLevelNumber].numberOfSpheresToSpawn);
        gameplayComing = true;
        StartCoroutine(ChangeColorOfSpawnedSpheres());
    }

    IEnumerator DelayedRespawn(GameObject sphereEllipce)
    {
        yield return new WaitForSecondsRealtime(1f);

        listOfSpotsForSphereSpawn.position = startPosition;
        sphereEllipce.SetActive(false);

        MethodForRestartLevel();
    }

    IEnumerator ChangeColorOfSpawnedSpheres()
    {
        while(gameplayComing)
        {
            yield return new WaitForSecondsRealtime(4f);

            MethodForSettingColorsOnSpawnedSpheres(infoAboutLevels.LevelsStructs[LevelsManager.varForSingleton.choosedLevelNumber].numberOfSpheresToSpawn);
        }
    }

    public void MethodForRestartLevel()
    {
        StopAllCoroutines();
        MethodForSpawningNewSpheres();
    }

    public void StopGamePlay(bool newGameStatement)
    {
        StopAllCoroutines();
        gameplayComing = newGameStatement;
    }
}
