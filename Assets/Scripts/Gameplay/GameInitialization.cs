using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInitialization : MonoBehaviour
{
    [SerializeField]
    private Text goldNumText;
    void Start()
    {
        GENERAL.goldNumText = goldNumText;
        GENERAL.GOLD = 25;
    }

    private void FixedUpdate()
    {
        if(StepCounter.currentStep % 50 == 0)
        {
            GENERAL.GOLD++;
        }
    }
}
