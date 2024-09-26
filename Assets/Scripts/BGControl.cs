using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGControl : MonoBehaviour
{
    public Transform BGPanelsHolder;

    public void ChangeBG(int bgID)
    {
        for (int i = 0; i < BGPanelsHolder.childCount; i++)
        {
            BGPanelsHolder.GetChild(i).gameObject.SetActive(false);
        }

        BGPanelsHolder.GetChild(bgID).gameObject.SetActive(true);
    }
}
