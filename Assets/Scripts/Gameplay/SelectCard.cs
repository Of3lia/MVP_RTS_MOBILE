using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCard : MonoBehaviour
{
    public void SelectMiner()
    {
        GENERAL.SELECTED_CARD = "CreateMiner";
        GENERAL.UNIT_COST = 15;
    }

    public void SelectSwordsman()
    {
        GENERAL.SELECTED_CARD = "CreateSwordsman";
        GENERAL.UNIT_COST = 60;
    }
}
