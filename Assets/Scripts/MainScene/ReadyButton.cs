using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyButton : MonoBehaviour
{
    [SerializeField]
    private int id;
    private bool isReady;
    public string isReadyString = "0";

    private void Update()
    {
        Debug.Log(GENERAL.PLAYER);
        Debug.Log(GENERAL.ISREADY);
    }
    public void Toggle()
    {
        if (GENERAL.PLAYER == id)
        {
            transform.GetChild(1).gameObject.SetActive(isReady);
            isReady = !isReady;
            transform.GetChild(0).gameObject.SetActive(isReady);
            GENERAL.ISREADY = isReady ? 1 : 0;
        }
    }
    public void SetReady(bool value)
    {
        transform.GetChild(0).gameObject.SetActive(value);
        transform.GetChild(1).gameObject.SetActive(!value);
    }
}
