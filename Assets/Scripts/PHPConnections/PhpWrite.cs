using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class PhpWrite : MonoBehaviour
{
    private string action = "EmptyAction;";
    private WaitForSecondsRealtime waitTime;
    [SerializeField]
    private UserConsole userConsoleText;
    [SerializeField]
    private Transform creationFlag;
    private Transform creationFlagClone;

    private string _uri;

    private void Awake()
    {
        _uri = GENERAL.SERVER + "Write.php";
    }

    private void Start()
    {
        waitTime = new WaitForSecondsRealtime(1);
        StartCoroutine(WritePhp());
    }

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateSelectedCard_Click();
            }
        } 
    }

    private void CreateSelectedCard_Click()
    {
        if (GENERAL.SELECTED_CARD != "")
        {
            if (GENERAL.GOLD < GENERAL.UNIT_COST)
            {
                StartCoroutine(userConsoleText.WriteText("Not enough gold to create unit", Color.red));
            }
            else
            {
                creationFlagClone = Instantiate(creationFlag);
                creationFlagClone.position = new Vector2(
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                Destroy(creationFlagClone.gameObject, 5);

                GENERAL.GOLD -= GENERAL.UNIT_COST;

                if (action == "EmptyAction;")
                {
                    action = "actionPla_yer" + GENERAL.PLAYER + ";actionSt_ep" + StepCounter.currentStep + ";posx" + Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
                        + ";posy" + Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y) + ";" + GENERAL.SELECTED_CARD + ";";
                }
                else
                {
                    action += "actionPla_yer" + GENERAL.PLAYER + ";actionSt_ep" + StepCounter.currentStep +
                        /*";step" + StepCounter.currentStep + */ ";player" + GENERAL.PLAYER +
                        ";posx" + Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
                        + ";posy" + Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y) + ";" + GENERAL.SELECTED_CARD + ";";
                }
            }
            
        }
    }

    public IEnumerator WritePhp()
    {
        while (true)
        {
            if (GameMenu.GAME_STARTED)
            {
                WWWForm form = new WWWForm();
                form.AddField("step", StepCounter.currentStep);
                form.AddField("player", GENERAL.PLAYER);
                form.AddField("action", action);
                form.AddField("room", GENERAL.ROOM);

                #if UNITY_EDITOR
                    form.AddField("room", "test_room");
                #endif
                using (UnityWebRequest webRequest = UnityWebRequest.Post(_uri, form))
                {
                    yield return webRequest.SendWebRequest();

                    if (webRequest.isNetworkError || webRequest.isHttpError)
                    {
                        Debug.Log(webRequest.error);
                    }
                    else
                    {
                        //Debug.Log("Form upload complete!");

                        //Debug.Log(webRequest.downloadHandler.text);
                    }
                }
                action = "EmptyAction;";
            }
            yield return waitTime;
        }
    }
}
