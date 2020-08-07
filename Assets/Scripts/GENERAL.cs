using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

static class GENERAL
{
#if UNITY_EDITOR
    public static string SERVER = "http://192.168.1.131/UNITY_PROJECTS/ClashRoyaleTry/";
#else
    public static string SERVER = "http://192.168.1.131/UNITY_PROJECTS/ClashRoyaleTry/";

    //public static string SERVER = "https://tutorialbasesdedatos.000webhostapp.com/";
#endif
    public static int PLAYER = 1;
    public static int ISREADY = 0;
    public static string ROOM = "";
    public static string SELECTED_CARD = "";
    public static int UNIT_COST;

    private static int gold;
    public static int GOLD
    {
        get => gold;
        set { gold = value; goldNumText.text = value.ToString(); }
    }
    public static Text goldNumText;
    
    //public static bool TESTING_MODE = false;

    public static int Scene_Opening = 0;
    public static int Scene_RegisterLogin = 1;
    public static int Scene_MainMenuScene = 2;
    public static int Scene_GameScene = 3;
}
