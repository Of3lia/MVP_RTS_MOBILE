using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningScene : MonoBehaviour
{
    private void Start()
    {
        Screen.sleepTimeout = 150;
        Invoke("LoadMenu", 2);
        Time.timeScale = 1;
    }

    private void LoadMenu()
    {
        if (PlayerPrefs.HasKey("userName"))
        {
            SceneManager.LoadScene(GENERAL.Scene_MainMenuScene);
        }
        else
        {
            SceneManager.LoadScene(GENERAL.Scene_RegisterLogin);
        }
    }
}
