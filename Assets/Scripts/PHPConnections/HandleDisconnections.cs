using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandleDisconnections : MonoBehaviour
{
    [SerializeField]
    private GameObject reconectingPanel;

    private int waitSecondsForDisconnection = 4;

    private int stepsToWaitBeforeLostConnection = 80;

    private WaitForSecondsRealtime waitTime;
    private WaitForSecondsRealtime waitTime2;
    public static string P1_LAST_UNIX_TIME;
    public static string P2_LAST_UNIX_TIME;

    public static string p1CurrentStep;
    public static string p2CurrentStep;

    public static bool connected;

    //[SerializeField]
    private bool developingMode = false;

    private void Awake()
    {
        Application.targetFrameRate = 20;
        Time.timeScale = 0;
        //GENERAL.TESTING_MODE = developingMode;

        #if UNITY_EDITOR
            developingMode = true;
        # endif
    }
    private void Start()
    {
        connected = false;
        p1CurrentStep = "0";
        p2CurrentStep = "0";
        P1_LAST_UNIX_TIME = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        P2_LAST_UNIX_TIME = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

        waitTime = new WaitForSecondsRealtime(0.5f);
        //waitTime2 = new WaitForSecondsRealtime(3);

        StartCoroutine(CheckIfConnectionLost());
    }

    private IEnumerator CheckIfConnectionLost()
    {
        while (true)
        {
            if(GENERAL.PLAYER == 1)
            {
                if(StepCounter.currentStep - int.Parse(p2CurrentStep) > stepsToWaitBeforeLostConnection)
                {
                    if (!developingMode)
                    {
                        ConnectionLost();
                    }
                }
                else if (!connected && GameMenu.GAME_STARTED)
                {
                    RestoreGame();
                }
            }
            if(GENERAL.PLAYER == 2)
            {
                if (StepCounter.currentStep - int.Parse(p1CurrentStep) > stepsToWaitBeforeLostConnection)
                {
                    if (!developingMode)
                    {
                        ConnectionLost();
                    }
                }
                else if (!connected && GameMenu.GAME_STARTED)
                {
                    RestoreGame();
                }
            }
            yield return waitTime;
        }
    }

    private void ConnectionLost()
    {
        if (connected)
        {
            connected = false;
            reconectingPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void RestoreGame()
    {
        //Debug.Log("Restoring Game!");
        connected = true;
        reconectingPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
