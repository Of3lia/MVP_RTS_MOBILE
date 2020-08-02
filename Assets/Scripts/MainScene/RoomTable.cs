using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RoomTable : MonoBehaviour
{
    [SerializeField]
    private Text idText, p1NameText, p2NameText;
    [SerializeField]
    private GameObject joinButton, youAreHereButton;
    public string Id { get { return idText.text; } set { idText.text = value; } }
    public string P1Name {
        get { return p1NameText.text; }
        set { p1NameText.text = value;
            if (p1NameText.text == PlayerPrefs.GetString("userName")) { youAreHereButton.SetActive(true); joinButton.SetActive(false); }
        } }

    public string P2Name
    {
        get { return p2NameText.text; }
        set
        {
            p2NameText.text = value;
            if (p2NameText.text == PlayerPrefs.GetString("userName")) { youAreHereButton.SetActive(true); joinButton.SetActive(false); }
            if (GENERAL.PLAYER == 2) { if (p2NameText.text == "") { youAreHereButton.SetActive(false); joinButton.SetActive(true); } }
        } }

    private LobbyManager lobbyManager;
    private MainScenePanelManager panelManager;
    private RoomManager roomManager;
    private void Awake()
    {
        lobbyManager = GameObject.Find("SCENE_MANAGER").gameObject.GetComponent<LobbyManager>();
        panelManager = GameObject.Find("SCENE_MANAGER").gameObject.GetComponent<MainScenePanelManager>();
        roomManager = GameObject.Find("SCENE_MANAGER").gameObject.GetComponent<RoomManager>();
    }

    public void JoinRoom()
    {
        if (joinButton.activeSelf)
        {
            StartCoroutine(JoinHostedRoom());
            roomManager.getRoom = true;
        }
        panelManager.GoToRoomPanel();
    }

    public IEnumerator JoinHostedRoom()
    {
        GENERAL.ROOM = Id;
        GENERAL.PLAYER = 2;
        WWWForm form = new WWWForm();

        form.AddField("room_id", GENERAL.ROOM);
        form.AddField("name", PlayerPrefs.GetString("userName"));

        using (UnityWebRequest www = UnityWebRequest.Post(GENERAL.SERVER + "joinroom.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Form upload complete!");
                // Debug.Log(www.downloadHandler.text);

                string downloadedText = www.downloadHandler.text;
                Debug.Log(downloadedText);

                if (downloadedText == "")
                {
                    //GENERAL.ROOM = downloadedText;
                }
            }

        }
        panelManager.GoToRoomPanel();
    }
}
