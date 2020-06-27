using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PhpRead : MonoBehaviour
{
    public Transform p1Units;
    public Transform p2Units;

    public Transform p1Swordsman;
    public Transform p2Swordsman;

    private Transform createdUnit;

    private WaitForSeconds waitTime;
    private WaitForFixedUpdate waitForFixedUpdate;

    private string downloadedStrings = "";
    //private List<string> actions;
    private string strings;

    private void Start()
    {
        waitTime = new WaitForSeconds(0.5f);
        waitForFixedUpdate = new WaitForFixedUpdate();

        //actions = new List<string>();

        if (!PlayerPrefs.HasKey("id"))
        {
            PlayerPrefs.SetInt("id", 0);
        }
        StartCoroutine(ReadPhp());
    }

    private void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("id"));
    }

    private IEnumerator ReadPhp()
    {
        while (true)
        {
            WWWForm form = new WWWForm();
            form.AddField("id", PlayerPrefs.GetInt("id"));

            using (UnityWebRequest webRequest = UnityWebRequest.Post("http://192.168.1.130/UNITY_PROJECTS/ClashRoyaleTry/Read.php", form))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    Debug.Log("Network Error");
                }
                else
                {
                    string downloadedText = webRequest.downloadHandler.text;

                    Debug.Log(downloadedText);

                    if (downloadedText != "")
                    {
                        for (int i = 0; i < downloadedText.Length; i++)
                        {
                            switch (downloadedStrings)
                            {
                                case "id":
                                    if (downloadedText[i] != ';')
                                    {
                                        strings += downloadedText[i];
                                    }
                                    else
                                    {
                                        if(int.Parse(strings) > PlayerPrefs.GetInt("id"))
                                        {
                                            PlayerPrefs.SetInt("id", int.Parse(strings));
                                        }
                                        strings = "";
                                        downloadedStrings = "";
                                    }
                                    break;

                                case "step":
                                    if (downloadedText[i] != ';')
                                    {
                                        strings += downloadedText[i];
                                    }
                                    else
                                    {
                                        StartCoroutine(CreateUnit(int.Parse(strings)));
                                        strings = "";
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
            yield return waitTime;
        }
    }

    private IEnumerator CreateUnit(int step)
    {
        Debug.Log(step);
        int stepToCheck = step + 50;
        Debug.Log("Create Soldier!");
        while(true)
        {
            if(stepToCheck == StepCounter.currentStep)
            {
                InstantiateUnit(p1Swordsman, p1Units, 0, 0);
                yield break;
            }
            yield return waitForFixedUpdate;
        }
    }

    private void InstantiateUnit(Transform unit, Transform parent, int posx, int posy)
    {
        Debug.Log("Soldier Created!");

        createdUnit = Instantiate(unit, parent);
        createdUnit.position = new Vector2(posx, posy);
    }
}
