using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainPanel;
    public static bool GAME_STARTED;
    private void Start()
    {
        GAME_STARTED = false;
    }
    public void SelectP1Button()
    {
        GENERAL.PLAYER = 1;
    }

    public void SelectP2Button()
    {
        GENERAL.PLAYER = 2;
    }

    public void StartButton()
    {
        mainPanel.SetActive(false);
        GAME_STARTED = true;
    }

    public void MenuButton_Click()
    {
        mainPanel.SetActive(true);
    }

    public void QuitGame_Click()
    {
        SceneManager.LoadScene(GENERAL.Scene_MainMenuScene);
    }

    public void DisableFowOfWar()
    {
        UnityEngine.Experimental.Rendering.Universal.Light2D TerrainLight = GameObject.Find("Terrain").gameObject.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        TerrainLight.enabled = !TerrainLight.isActiveAndEnabled;
    }

    public void ShowUnitsStats()
    {
        Time.timeScale = 0;
    }
}
