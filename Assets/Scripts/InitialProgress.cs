using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class InitialProgress : MonoBehaviour
{
    public Transform buttonsHolder, bgHolder;
    public ClassForInformationAboutLevel lvlInfo;

    public void Awake()
    {
        PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("Progress", 3);
        CheckProgress();
    }

    public void CheckProgress()
    {
        if(PlayerPrefs.HasKey("Progress"))
        {
            CheckCycle(buttonsHolder, 1, 1, (i) =>
            {
                buttonsHolder.GetChild(i).GetComponent<Button>().enabled = true;
                if(i - 1 >= 0)
                {
                    buttonsHolder.GetChild(i - 1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = lvlInfo.LevelsStructs[i].goalPoints.ToString();
                }
                buttonsHolder.GetChild(i).GetChild(1).gameObject.SetActive(true);

            });

            CheckCycle(bgHolder, 3, 0, (i) =>
            {
                bgHolder.GetChild(i).GetChild(1).GetComponent<Button>().enabled = true;
            });
        }
    }

    private void CheckCycle(Transform holder, int a, int b, Action<int> additionalAct = null)
    {
        for (int i = 0; i < holder.childCount; i++)
        {
            if ((holder.GetChild(i).GetSiblingIndex() * a) + b <= PlayerPrefs.GetInt("Progress"))
            {
                additionalAct?.Invoke(i);

                //holder.GetChild(i).GetComponent<Button>().enabled = true;

                //if(holder == buttonsHolder)
                //{
                //    holder.GetChild(i).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = lvlInfo.LevelsStructs[i + 1].goalPoints.ToString();
                //    holder.GetChild(i).GetChild(1).gameObject.SetActive(true);
                //}
            }
        }
    }
}
