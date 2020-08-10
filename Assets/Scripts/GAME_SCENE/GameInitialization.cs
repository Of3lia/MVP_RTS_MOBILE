using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInitialization : MonoBehaviour
{
    [SerializeField]
    private Text goldNumText;

    [SerializeField]
    GameObject p1Camera;
    [SerializeField]
    GameObject p2Camera;

    [SerializeField]
    Transform p1UnitsPool;
    [SerializeField]
    Transform p2UnitsPool;

    Transform p1Units;
    Transform p2Units;

    void Start()
    {
        p1Units = GetComponent<SharedFields>().p1Units;
        p2Units = GetComponent<SharedFields>().p2Units;

        GENERAL.goldNumText = goldNumText;
        GENERAL.GOLD = 150;

        if (GENERAL.PLAYER == 1)
        {
            p1Camera.SetActive(true);
            p2Camera.SetActive(false);

            RotateTextOfUnitsInGameScene(p2Units);
        }
        else if(GENERAL.PLAYER == 2)
        {
            p1Camera.SetActive(false);
            p2Camera.SetActive(true);

            RotateTextOfUnitsInGameScene(p1Units);
            RotateTextOfUnitsInPool(p1UnitsPool);
        }
    }

    private void FixedUpdate()
    {
        if (StepCounter.currentStep % 50 == 0)
        {
            GENERAL.GOLD++;
        }
    }

    private void RotateTextOfUnitsInGameScene(Transform unitsContainer)
    {
        for(int i = 0; i < unitsContainer.childCount; i++)
        {
            unitsContainer.GetChild(i).GetComponent<SharedComponents>().hpSlider.transform.parent.Rotate(0, 0, 180);
        }
    }
    private void RotateTextOfUnitsInPool(Transform poolsContainer)
    {
        for (int i = 0; i < poolsContainer.childCount; i++)
        {
            for (int j = 0; j < poolsContainer.GetChild(i).childCount; j++)
            {
                poolsContainer.GetChild(i).GetChild(j).GetComponent<SharedComponents>().hpSlider.transform.parent.Rotate(0,0,180);
            }
        }
    }
}
