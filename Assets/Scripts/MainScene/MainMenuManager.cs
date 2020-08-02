using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        GENERAL.PLAYER = 1;
        GENERAL.ISREADY = 0;
        GENERAL.ROOM = "";
        GENERAL.SELECTED_CARD = "";
    }
    public void ExitAccount_Click()
    {
        SceneManager.LoadScene(GENERAL.Scene_RegisterLogin);
        PlayerPrefs.DeleteAll();
    }
}
