using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    public DelegateHolder delegateHolder;
    public SpawnSpheresClass spawnSpheres;
    public Transform sphereSpawn;
    public GameObject plateSmall, plateBig, gamePanel;
    public Image internTargetImage;
    public List<GameObject> instructionsText;
    public bool interact;

    private void OnEnable()
    {
        plateSmall.SetActive(true);

        interact = false;

        StartCoroutine(SwitchInstructions());
    }

    private IEnumerator SwitchInstructions()
    {
        yield return new WaitForSeconds(3);

        interact = true;
        plateSmall.SetActive(false);
        plateBig.SetActive(true);
        instructionsText[0].SetActive(true);

        delegateHolder.InvokeOnMakePositions(0.1f, 0.9f, 0.6f, 0.8f, false);

        internTargetImage.sprite = sphereSpawn.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    }

    public IEnumerator SwitchInstructionTexts()
    {
        yield return new WaitForSeconds(3);

        instructionsText[1].SetActive(false);
        instructionsText[2].SetActive(true);

        List<GameObject> forMethod = spawnSpheres.MethodForNotRepeating(3);

        for (int i = 0; i < sphereSpawn.childCount; i++)
        {
            sphereSpawn.GetChild(i).GetComponent<SpriteRenderer>().sprite = forMethod[i].GetComponent<SpriteRenderer>().sprite;
            sphereSpawn.GetChild(i).GetComponent<CircleCollider2D>().radius = forMethod[i].GetComponent<CircleCollider2D>().radius;
            sphereSpawn.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite = forMethod[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        }

        sphereSpawn.GetChild(0).GetChild(0).gameObject.SetActive(false);

        internTargetImage.sprite = sphereSpawn.GetChild(0).GetComponent<SpriteRenderer>().sprite;

        yield return new WaitForSeconds(3);

        for (int i = 0; i < sphereSpawn.childCount; i++)
        {
            Destroy(sphereSpawn.GetChild(i).gameObject);
        }

        yield return null;

        delegateHolder.InvokeNextLevel(1);

        gameObject.SetActive(false);
    }
}
