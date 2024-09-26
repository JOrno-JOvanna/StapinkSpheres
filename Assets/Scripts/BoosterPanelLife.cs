using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterPanelLife : MonoBehaviour
{
    private void OnEnable()
    {
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("booster");
        StartCoroutine(WaitAnimation());
    }

    private IEnumerator WaitAnimation()
    {
        yield return new WaitForSeconds(1.5f);

        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("booster");
    }
}
