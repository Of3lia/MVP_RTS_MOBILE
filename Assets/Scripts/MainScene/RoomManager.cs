using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RoomManager : MonoBehaviour
{
    WaitForSecondsRealtime waitSec;

    string p1Name, p2Name, lastDownloadedText, charsAddition;
    string p1IsReady, p2IsReady;

    [SerializeField]
    private Text p1_name, p2_name;
    [SerializeField]
    private ReadyButton p1IsReadyButton, p2IsReadyButton;

    public bool getRoom;

    private MainScenePanelManager panelManager;

    private void Start()
    {
        panelManager = GetComponent<MainScenePanelManager>();
        waitSec = new WaitForSecondsRealtime(2);
        StartCoroutine(GetRoom());
    }

    public IEnumerator HostGameRoom()
    {
        if (GENERAL.ROOM == "")
        {
            GENERAL.PLAYER = 1;
            WWWForm form = new WWWForm();
            form.AddField("name", PlayerPrefs.GetString("userName"));

            using (UnityWebRequest www = UnityWebRequest.Post(GENERAL.SERVER + "hostroom.php", form))
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

                    if (downloadedText != "")
                    {
                        GENERAL.ROOM = downloadedText;

                        getRoom = true;
                    }
                }
            }
        }
    }

    public IEnumerator GetRoom()
    {
        while (true)
        {
            while (getRoom)
            {
                if (GENERAL.ROOM != "")
                {
                    WWWForm form = new WWWForm();
                    form.AddField("id", GENERAL.ROOM);
                    form.AddField("player", GENERAL.PLAYER);
                    form.AddField("is_ready", GENERAL.ISREADY);

                    using (UnityWebRequest www = UnityWebRequest.Post(GENERAL.SERVER + "getroom.php", form))
                    {
                        yield return www.SendWebRequest();

                        if (www.isNetworkError || www.isHttpError)
                        {
                            Debug.Log(www.error);
                        }
                        else
                        {
                            //Debug.Log("Form upload complete!");
                            //Debug.Log(www.downloadHandler.text);

                            string downloadedText = www.downloadHandler.text;

                            if (downloadedText != lastDownloadedText)
                            {
                                lastDownloadedText = downloadedText;
                                charsAddition = "";
                                p1Name = "";
                                p2Name = "";

                                for (int i = 0; i < downloadedText.Length; i++)
                                {
                                    switch (charsAddition)
                                    {
                                        case "p1_name":

                                            if (downloadedText[i] != ';')
                                                p1Name += downloadedText[i];
                                            else
                                            {
                                                p1_name.text = p1Name;
                                                charsAddition = "";
                                                p1Name = "";
                                            }
                                            break;

                                        case "p1_is_ready":
                                            if (downloadedText[i] != ';')
                                                p1IsReady += downloadedText[i];
                                            else
                                            {
                                                if (p1IsReady == "1") { p1IsReadyButton.isReadyString = "1"; }
                                                if (GENERAL.PLAYER != 1)
                                                {
                                                    if (p1IsReady == "1") { p1IsReadyButton.SetReady(true);  }
                                                    else { p1IsReadyButton.SetReady(false); p1IsReadyButton.isReadyString = "0"; }
                                                }
                                                p1IsReady = "";
                                                charsAddition = "";
                                            }
                                            break;

                                        case "p2_name":

                                            if (downloadedText[i] != ';')
                                                p2Name += downloadedText[i];
                                            else
                                            {
                                                p2_name.text = p2Name;
                                                charsAddition = "";
                                                p2Name = "";
                                            }
                                            break;

                                        case "p2_is_ready":
                                            if (downloadedText[i] != ';')
                                                p2IsReady += downloadedText[i];
                                            else
                                            {
                                                if(p2IsReady == "1") { p2IsReadyButton.isReadyString = "1"; }
                                                if (GENERAL.PLAYER != 2)
                                                {
                                                    if (p2IsReady == "1") { p2IsReadyButton.SetReady(true);  }
                                                    else { p2IsReadyButton.SetReady(false); p2IsReadyButton.isReadyString = "0"; }
                                                }
                                                p2IsReady = "";
                                                charsAddition = "";
                                            }
                                            break;
                                        default:

                                            charsAddition += downloadedText[i];
                                            break;
                                    }
                                }
                            }
                            // Load GAME SCENE
                            if (p1IsReadyButton.isReadyString == "1" && p2IsReadyButton.isReadyString == "1")
                            {
                                SceneManager.LoadScene(GENERAL.Scene_GameScene);
                            }
                        }
                    }
                }
                yield return waitSec;
            }
            yield return waitSec;
        }
    }

    public void LeaveRoomButton_Click()
    {
        StartCoroutine(LeaveRoom());
        getRoom = false;
        GENERAL.ISREADY = 0;
    }

    private IEnumerator LeaveRoom()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", PlayerPrefs.GetString("userName"));
        form.AddField("player", GENERAL.PLAYER);

        using (UnityWebRequest www = UnityWebRequest.Post(GENERAL.SERVER + "leaveroom.php", form))
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
        GENERAL.ROOM = "";
        panelManager.GoToLobbyPanel();
    }

}
