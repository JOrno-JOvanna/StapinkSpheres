using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnSpheresClass : MonoBehaviour
{
    public DelegateHolder delegateHolder;
    public Transform holder, sphereSpawn;
    public float spheresSpeed;
    public TextMeshProUGUI goalScoreText, levelNumberText;

    [SerializeField]private protected int levelNumber, goalScore, targetImageID;
    public LevelDifficulty levelDifficulty;
    private protected int numberOfSpheresToSpawn;
    private float minDistance;
    private protected List<Vector2> spawnPositions = new List<Vector2>();
    private protected List<float> sphereSpeeds = new List<float>();

    private void Start()
    {
        delegateHolder.OnMakePositions += MakeRandomPositions;
        delegateHolder.SpawnSpheres += SpawnRandomSpheres;
        delegateHolder.SetDifficulty += SetDifficulty;
        delegateHolder.OnExitGame += ExitGame;
        delegateHolder.OnPauseGame += OnPauseGame;
    }

    private void SetDifficulty(LevelDifficulty difficulty)
    {
        levelDifficulty = difficulty;
    }

    public void MakeRandomPositions(float down1, float up1, float down2, float up2, bool changecolor)
    {
        StopAllCoroutines();
        spawnPositions.Clear();

        if (sphereSpawn.childCount != 0)
        {
            for (int i = 0; i < sphereSpawn.childCount; i++)
            {
                Destroy(sphereSpawn.GetChild(i).gameObject);
            }
        }

        numberOfSpheresToSpawn = Random.Range(levelDifficulty.MinNumberOfSpheres, levelDifficulty.MaxNumberOfSpheres + 1);
        minDistance = levelDifficulty.DistanceBetweenSpheres;

        for (int i = 0; i < 100; i++)
        {
            Vector2 randomViewportPosition = new Vector2(Random.Range(down1, up1), Random.Range(down2, up2));

            Vector3 worldPosition = Camera.main.ViewportToWorldPoint(new Vector3(randomViewportPosition.x, randomViewportPosition.y, 0));

            bool isFarEnough = true;
            foreach (Vector2 pos in spawnPositions)
            {
                if (Vector2.Distance(worldPosition, pos) < minDistance)
                {
                    isFarEnough = false;
                    break;
                }
            }

            if (isFarEnough)
            {
                spawnPositions.Add(worldPosition);
            }

            if (spawnPositions.Count == numberOfSpheresToSpawn)
                break;
        }

        SpawnRandomSpheres(numberOfSpheresToSpawn, changecolor);
    }

    public void SpawnRandomSpheres(int numberOfObjectsToSpawn, bool changeColor)
    {
        List<GameObject> forMethod = MethodForNotRepeating(numberOfObjectsToSpawn);

        for (int i = 0; i < forMethod.Count; i++)
        {
            Instantiate(forMethod[i], spawnPositions[i], Quaternion.identity, sphereSpawn);
        }

        if(changeColor)
        {
            StartCoroutine(ChangeSpheresColor());
        }
    }

    public List<GameObject> MethodForNotRepeating(int numberOfObjectsToSpawn)
    {
        List<GameObject> spawnedSpheres = new List<GameObject>();
        List<int> spheresID = new List<int>();
        for (int i = 0; i < holder.childCount; i++)
        {
            spheresID.Add(i);
        }

        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            int sphereID = spheresID[Random.Range(0, spheresID.Count)];

            spheresID.Remove(sphereID);

            spawnedSpheres.Add(holder.GetChild(sphereID).gameObject);
        }

        return spawnedSpheres;
    }

    private void ReorderSpheres()
    {
        for (int i = 0; i < sphereSpawn.childCount; i++)
        {
            sphereSpawn.GetChild(i).SetSiblingIndex(i + 1);
        }
    }

    public IEnumerator ChangeSpheresColor()
    {
        while(sphereSpawn.childCount > 0)
        {
            yield return new WaitForSeconds(4f);

            List<GameObject> forMethod = MethodForNotRepeating(numberOfSpheresToSpawn);

            for (int i = 0; i < sphereSpawn.childCount; i++)
            {
                sphereSpawn.GetChild(i).GetComponent<SpriteRenderer>().sprite = forMethod[i].GetComponent<SpriteRenderer>().sprite;
                sphereSpawn.GetChild(i).GetComponent<CircleCollider2D>().radius = forMethod[i].GetComponent<CircleCollider2D>().radius;
                sphereSpawn.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite = forMethod[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            }

            ReorderSpheres();
        }
    }

    private void OnPauseGame()
    {
        StopAllCoroutines();
    }

    private void ExitGame()
    {
        levelDifficulty = null;
    }

    private void OnApplicationQuit()
    {
        delegateHolder.OnMakePositions -= MakeRandomPositions;
        delegateHolder.SpawnSpheres -= SpawnRandomSpheres;
        delegateHolder.SetDifficulty -= SetDifficulty;
        delegateHolder.OnExitGame -= ExitGame;
        delegateHolder.OnPauseGame -= OnPauseGame;
    }
}
