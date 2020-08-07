using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PhpRead : MonoBehaviour
{

    #region Fields Declarations

    //public Transform p1Units;
    //public Transform p2Units;

    // public Transform p1Swordsman;
    //public Transform p2Swordsman;

    //public Transform p1Miner;
    //public Transform p2Miner;

    UnitsPoolManager poolManager;

    private Transform createdUnit;

    private WaitForSecondsRealtime waitTime;
    private WaitForFixedUpdate waitForFixedUpdate;

    private string downloadedStrings = "";
    private string strings;
    private string player;
    private string id = "";
    private string posx;
    private string posy;
    private string actionStep = "0";
    private string actionPlayer = "";
    private string step = "0";
    private string p1LastUnixTime;
    private string p2LastUnixTime;
    private string p1Step;
    private string p2Step;

    float timeRemaining;

    private string _uri;

    #endregion


    private void Awake()
    {
        _uri = GENERAL.SERVER + "read.php";
    }

    private void Start()
    {
        waitTime = new WaitForSecondsRealtime(1);
        waitForFixedUpdate = new WaitForFixedUpdate();

        PlayerPrefs.SetInt("id", 0);

        StartCoroutine(ReadPhp());

        poolManager = GetComponent<UnitsPoolManager>();
    }

    private IEnumerator ReadPhp()
    {
        while (true)
        {
            if (GameMenu.GAME_STARTED)
            {
                WWWForm form = new WWWForm();
                form.AddField("id", PlayerPrefs.GetInt("id"));
                form.AddField("room", GENERAL.ROOM);

                #if UNITY_EDITOR
                    form.AddField("room", "test_room");
                #endif

                using (UnityWebRequest webRequest = UnityWebRequest.Post( _uri , form))
                {
                    yield return webRequest.SendWebRequest();

                    if (webRequest.isNetworkError)
                    {
                        Debug.Log("Network Error");
                    }
                    else
                    {
                        string downloadedText = webRequest.downloadHandler.text;

                        //Debug.Log(downloadedText);

                        if (downloadedText != "")
                        {
                            downloadedStrings = "";
                            player = "";
                            step = "";
                            id = "";
                            posx = "";
                            posy = "";
                            strings = "";
                            actionPlayer = "";
                            actionStep = "";

                            for (int i = 0; i < downloadedText.Length; i++)
                            {
                                //Debug.Log(downloadedText[i]);
                                //Debug.Log(downloadedStrings);

                                switch (downloadedStrings)
                                {
                                    case "id":
                                        if (downloadedText[i] != ';')
                                        {
                                            id += downloadedText[i];
                                            //Debug.Log(id);
                                        }
                                        else
                                        {
                                            PlayerPrefs.SetInt("id", int.Parse(id));
                                            id = "";
                                            downloadedStrings = "";
                                        }
                                        break;

                                    case "player":
                                        if (downloadedText[i] != ';')
                                        {
                                            player += downloadedText[i];
                                        }
                                        else
                                        {
                                            downloadedStrings = "";
                                        }
                                        break;

                                    case "unix_time":
                                        if (player == "1")
                                        {
                                            if (downloadedText[i] != ';')
                                            {
                                                p1LastUnixTime += downloadedText[i];
                                            }
                                            else
                                            {
                                                HandleDisconnections.P1_LAST_UNIX_TIME = p1LastUnixTime;
                                                p1LastUnixTime = "";
                                                downloadedStrings = "";
                                                break;
                                            }
                                        }
                                        else if (player == "2")
                                        {
                                            if (downloadedText[i] != ';')
                                            {
                                                p2LastUnixTime += downloadedText[i];
                                            }
                                            else
                                            {
                                                HandleDisconnections.P2_LAST_UNIX_TIME = p2LastUnixTime;
                                                p2LastUnixTime = "";
                                                downloadedStrings = "";
                                                break;
                                            }
                                        }
                                        break;

                                    case "posx":
                                        if (downloadedText[i] != ';')
                                        {
                                            posx += downloadedText[i];
                                        }
                                        else
                                        {
                                            downloadedStrings = "";
                                        }
                                        break;

                                    case "posy":
                                        if (downloadedText[i] != ';')
                                        {
                                            posy += downloadedText[i];
                                        }
                                        else
                                        {
                                            downloadedStrings = "";
                                        }
                                        break;

                                    case "step":
                                        if (player == "1")
                                        {
                                            if (downloadedText[i] != ';')
                                            {
                                                p1Step += downloadedText[i];
                                            }
                                            else
                                            {
                                                step = p1Step;
                                                HandleDisconnections.p1CurrentStep = p1Step;
                                                p1Step = "";
                                                downloadedStrings = "";
                                                break;
                                            }
                                        }
                                        else if (player == "2")
                                        {
                                            if (downloadedText[i] != ';')
                                            {
                                                p2Step += downloadedText[i];
                                            }
                                            else
                                            {
                                                step = p2Step;
                                                HandleDisconnections.p2CurrentStep = p2Step;
                                                p2Step = "";
                                                downloadedStrings = "";
                                                break;
                                            }
                                        }
                                        break;

                                    case "actionPla_yer":
                                        if (downloadedText[i] != ';')
                                        {
                                            actionPlayer += downloadedText[i];
                                        }
                                        else
                                        {
                                            downloadedStrings = "";
                                            break;
                                        }
                                        break;

                                    case "actionSt_ep":
                                        if (downloadedText[i] != ';')
                                        {
                                            actionStep += downloadedText[i];
                                        }
                                        else
                                        {
                                            downloadedStrings = "";
                                            break;
                                        }
                                        break;

                                    case "CreateMiner":
                                        if (downloadedText[i] != ';')
                                        {
                                            strings += downloadedText[i];
                                        }
                                        else
                                        {
                                            //Transform u;
                                            //if (actionPlayer == "1") { u = p1Miner; } else { u = p2Miner; }
                                            //Debug.Log($"actionStep{actionStep}\n actionplayer{actionPlayer}\n posx{posx} posy {posy}");
                                            StartCoroutine(CreateUnit(int.Parse(actionStep), int.Parse(actionPlayer), int.Parse(posx), int.Parse(posy), UnitsPoolManager.UnitsPool.Miner ));

                                            step = "";
                                            player = "";
                                            posx = "";
                                            posy = "";
                                            actionStep = "";
                                            actionPlayer = "";
                                            strings = "";
                                            downloadedStrings = "";
                                        }
                                        break;

                                    case "CreateSwordsman":
                                        if (downloadedText[i] != ';')
                                        {
                                            strings += downloadedText[i];
                                        }
                                        else
                                        {
                                            //Transform u;
                                            //if (actionPlayer == "1") { u = p1Swordsman; } else { u = p2Swordsman; }
                                            //Debug.Log($"actionStep{actionStep}\n actionplayer{actionPlayer}\n posx{posx} posy {posy}");
                                            StartCoroutine(CreateUnit(int.Parse(actionStep), int.Parse(actionPlayer), int.Parse(posx), int.Parse(posy), UnitsPoolManager.UnitsPool.Swordsman));

                                            posx = "";
                                            posy = "";
                                            actionStep = "";
                                            actionPlayer = "";
                                            strings = "";
                                            downloadedStrings = "";
                                        }
                                        break;

                                    case "EmptyAction":
                                        if (downloadedText[i] != ';')
                                        {

                                        }
                                        else
                                        {
                                            //Debug.Log("step:" + step + "player: " + player );
                                            step = "";
                                            player = "";
                                            posx = "";
                                            posy = "";
                                            strings = "";
                                            actionPlayer = "";
                                            actionStep = "";
                                            downloadedStrings = "";
                                        }
                                        break;

                                    default:
                                        downloadedStrings += downloadedText[i];
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            yield return waitTime;
        }
    }

    private IEnumerator CreateUnit(int actionStep, int player, int posx, int posy, UnitsPoolManager.UnitsPool unit)
    {
        //Debug.Log("Create Unit!");
        //Debug.Log(step);
        int stepToCheck = actionStep + 60;

        while (true)
        {
            if (stepToCheck == StepCounter.currentStep)
            {
                poolManager.ActivateUnitFromPool(player, posx, posy, unit);
                yield break;
            }
            yield return waitForFixedUpdate;
        }
    }

    /* private IEnumerator CreateUnit(int actionStep, int player, int posx, int posy, Transform _unitToCreate)
     {
         //Debug.Log("Create Unit!");
         //Debug.Log(step);
         int stepToCheck = actionStep + 100;

         Transform unitToCreate = _unitToCreate;
         Transform unitParent = p1Units;
         if(player == 1)
         {
         }
         else if (player == 2)
         {
             unitParent = p2Units;
         }
         while(true)
         {
             if(stepToCheck == StepCounter.currentStep)
             {
                 InstantiateUnit(unitToCreate, unitParent, posx, posy);
                 yield break;
             }
             yield return waitForFixedUpdate;
         }
     }*/

    /*private void InstantiateUnit(Transform unit, Transform parent, int posx, int posy)
    {
        //Debug.Log("Soldier Created!");

        createdUnit = Instantiate(unit, parent);
        createdUnit.position = new Vector2(posx, posy);
    }
    */

}
