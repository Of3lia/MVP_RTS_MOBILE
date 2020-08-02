using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScenePanelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject MainMenuPanel, LobbyPanel, RoomPanel, GameRoomButton;

    private void Start()
    {
        GoToMainMenuPanel();
    }
    public void GoToMainMenuPanel()
    {
        SwitchToPanel(MainMenuPanel);
    }
    public void GoToLobbyPanel()
    {
        SwitchToPanel(LobbyPanel);
    }
    public void GoToRoomPanel()
    {
        SwitchToPanel(RoomPanel);
        GameRoomButton.GetComponent<Button>().Select();
    }

    private void SwitchToPanel(GameObject panelToActivate)
    {
        panelToActivate.SetActive(true);
        if(MainMenuPanel != panelToActivate) { MainMenuPanel.SetActive(false); }
        if(LobbyPanel != panelToActivate) { LobbyPanel.SetActive(false); }
        if(RoomPanel != panelToActivate) { RoomPanel.SetActive(false); }
    }
}
