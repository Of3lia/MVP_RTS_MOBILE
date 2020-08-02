using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UserConsole : MonoBehaviour
{
    private WaitForSeconds sec;
    private Text userConsoleText;
    private void Start()
    {
        sec = new WaitForSeconds(3);
        userConsoleText = GetComponent<Text>();
    }

    public IEnumerator WriteText(string text, Color color)
    {
        userConsoleText.color = color;
        userConsoleText.text = text;
        yield return sec;
        userConsoleText.text = "";
    }
}
