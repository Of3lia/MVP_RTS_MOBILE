using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCard : MonoBehaviour
{
    public void SelectMiner()
    {
        GENERAL.SELECTED_CARD = "CreateMiner";
        //Debug.Log(GENERAL.SELECTED_CARD);
    }

    public void SelectSwordsman()
    {
        GENERAL.SELECTED_CARD = "CreateSwordsman";
        //Debug.Log(GENERAL.SELECTED_CARD);
    }
}
