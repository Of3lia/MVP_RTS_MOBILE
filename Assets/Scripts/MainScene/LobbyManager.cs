using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private Transform roomTables;
    RoomTable currentRoomTable;
    List<string> roomTablesActiveId;

    [SerializeField]
    private Transform noMoreRoomsText;

    bool activateGetLobby = true;
    private WaitForSecondsRealtime waitSec;
    string lastDownloadedText, charsAddition, p1Name, p2Name, id;

    private void Start()
    {
        roomTablesActiveId = new List<string>();
        waitSec = new WaitForSecondsRealtime(2);
        StartCoroutine(GetRooms());
    }

    public IEnumerator GetRooms()
    {
        while (activateGetLobby)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(GENERAL.SERVER + "getlobby.php"))
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
                        ManageRoomTables(downloadedText);
                    }
                }
            }
            yield return waitSec;
        }
    }

    private void ManageRoomTables(string downloadedText)
    {
        for(int i = 0; i < roomTables.childCount; i++)
        {
            roomTables.GetChild(i).gameObject.SetActive(false);
        }
        lastDownloadedText = downloadedText;
        charsAddition = "";
        id = "";
        p1Name = "";
        p2Name = "";
        for (int i = 0; i < downloadedText.Length; i++)
        {
            switch (charsAddition)
            {
                case "id":
                    if (downloadedText[i] != ';')
                    {
                        id += downloadedText[i];
                    }
                    else
                    {
                        roomTablesActiveId.Add(id);
                        currentRoomTable = roomTables.transform.GetChild(int.Parse(id)-1).GetComponent<RoomTable>();
                        currentRoomTable.gameObject.SetActive(true);
                        charsAddition = "";
                        id = "";
                    }
                    break;

                case "p1_name":

                    if (downloadedText[i] != ';')
                    {
                        p1Name += downloadedText[i];
                    }
                    else
                    {
                        currentRoomTable.P1Name = p1Name;
                        charsAddition = "";
                        p1Name = "";
                    }
                    break;

                case "p2_name":
                if(downloadedText[i] != ';')
                    {
                        p2Name += downloadedText[i];
                    }
                    else
                    {
                        currentRoomTable.P2Name = p2Name;
                        charsAddition = "";
                        p2Name = "";
                    }
                    break;

                default:

                    charsAddition += downloadedText[i];
                    break;
            }
        }
    }

    public void HostRoom()
    {
        StartCoroutine(GetComponent<RoomManager>().HostGameRoom());
        GetComponent<MainScenePanelManager>().GoToRoomPanel();
    }
}
