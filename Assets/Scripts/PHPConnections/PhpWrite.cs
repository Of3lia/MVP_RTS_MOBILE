using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PhpWrite : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(WritePhp());
        }
    }

    public IEnumerator WritePhp()
    {
        WWWForm form = new WWWForm();
        form.AddField("step", StepCounter.currentStep);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://192.168.1.130/UNITY_PROJECTS/ClashRoyaleTry/Write.php", form))
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
    }
}
