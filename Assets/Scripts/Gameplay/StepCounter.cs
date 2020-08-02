using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepCounter : MonoBehaviour
{
    public Transform p1Units;
    public Transform p2Units;

    public Transform p1Swordsman;
    public Transform p2Swordsman;

    private Transform createdUnit;

    public static int currentStep = 0;

    private WaitForFixedUpdate waitForFixedUpdate;

    private void Start()
    {
        currentStep = 0;
        waitForFixedUpdate = new WaitForFixedUpdate();
        StartCoroutine(FrameCounter());
    }

    private IEnumerator FrameCounter()
    {
        while (true)
        {
            if (HandleDisconnections.connected)
            {
                currentStep++;
            }
            yield return waitForFixedUpdate;

/*
            switch (currentStep)
            {
                case 28:
                    CreateUnit(p1Swordsman, p1Units, 0, 0);
                    break;
                case 31:
                    CreateUnit(p1Swordsman, p1Units, 1, 1);
                    break;
                case 59:
                    CreateUnit(p2Swordsman, p2Units, -1, 0);
                    break;
                case 65:
                    CreateUnit(p2Swordsman, p2Units, -2, -1);
                    break;
                case 80:
                    CreateUnit(p2Swordsman, p2Units, 0, 2);
                    break;
                case 98:
                    CreateUnit(p1Swordsman, p1Units, 0, 0);
                    break;
                case 101:
                    CreateUnit(p1Swordsman, p1Units, 1, 1);
                    break;
                case 102:
                    CreateUnit(p2Swordsman, p2Units, -1, 0);
                    break;
                case 120:
                    CreateUnit(p2Swordsman, p2Units, -2, -1);
                    break;
                case 125:
                    CreateUnit(p1Swordsman, p1Units, 0, 2);
                    break;
            }
  */
        }
    }

    private void CreateUnit(Transform unit, Transform parent, int posx, int posy)
    {
        createdUnit = Instantiate(unit, parent);
        createdUnit.position = new Vector2(posx, posy);
    }
}
