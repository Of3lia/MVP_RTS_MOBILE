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
    public void SelectArcher()
    {
        GENERAL.SELECTED_CARD = "CreateArcher";
        GENERAL.UNIT_COST = 50;
    }
    public void SelectKnight()
    {
        GENERAL.SELECTED_CARD = "CreateKnight";
        GENERAL.UNIT_COST = 200;
    }
    public void SelectScout()
    {
        GENERAL.SELECTED_CARD = "CreateScout";
        GENERAL.UNIT_COST = 45;
    }
    public void SelectTower()
    {
        GENERAL.SELECTED_CARD = "CreateTower";
        GENERAL.UNIT_COST = 350;
    }
    public void SelectTurtle()
    {
        GENERAL.SELECTED_CARD = "CreateTurtle";
        GENERAL.UNIT_COST = 280;
    }






}
