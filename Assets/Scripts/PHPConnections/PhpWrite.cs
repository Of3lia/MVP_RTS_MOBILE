using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class PhpWrite : MonoBehaviour
{

    #region Fields

    private string action = "EmptyAction;";
    private WaitForSecondsRealtime waitTime;
    [SerializeField]
    private UserConsole userConsoleText;
    [SerializeField]
    private Transform creationFlag;
    private Transform creationFlagClone;

    private delegate void InputToCheck();
    InputToCheck inputToCheck;

    private string _uri;

    #endregion

    private void Awake()
    {
        _uri = GENERAL.SERVER + "write.php";

        // Check if game is running on editor, windows or andoid

#if UNITY_EDITOR
        inputToCheck = CheckForClick;
# elif UNITY_STANDALONE
        inputToCheck = CheckForClick;
#else
        inputToCheck = CheckForTouch;
#endif
    }

    private void Start()
    {
        waitTime = new WaitForSecondsRealtime(1);
        StartCoroutine(CleanRoom());
        StartCoroutine(WritePhp());
    }

    private void Update()
    {
        ExecuteMethod(inputToCheck);
    }

    void ExecuteMethod(InputToCheck del)
    {
        del();
    }

    // This Method is for testing in unity editor
    private void CheckForClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                CreateSelectedCard_Click();
            }
        }
    }

    private void CheckForTouch()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                CreateSelectedCard_Click();
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
                Destroy(creationFlagClone.gameObject, 6);

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
                    action = "EmptyAction;";

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
            }
            yield return waitTime;
        }
    }

    public IEnumerator CleanRoom()
    {
        WWWForm form = new WWWForm();
        form.AddField("step", 0);
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
        yield return waitTime;
    }
}