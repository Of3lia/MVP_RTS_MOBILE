using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Miner : MobileUnit
{
    private Transform goldMines;
    private bool hasGold = false;
    private Transform castle;
    private int gatheringRate = 2;

    protected override void InitializeUnit()
    {
        base.InitializeUnit();

        goldMines = GameObject.Find("GoldMines").transform;

        if (CompareTag("1"))
        {
            castle = GameObject.Find("P1_Castle").transform;
        }
        else if (CompareTag("2"))
        {
            castle = GameObject.Find("P2_Castle").transform;
        }
        else
        {
            Debug.LogWarning("No tag assigned");
        }
    }

    private void FixedUpdate()
    {
        if (Time.frameCount % 10 == 0)
        {
            closestTarget = GetClosest(goldMines);
        }
        StateMachine();
        //Debug.Log(goldMines);
        //Debug.Log(castle);
    }

    protected override void StateMachine()
    {
        if (closestTarget != null && castle != null)
        {
            if (!hasGold)
            {
                WalkTo(closestTarget);

                if (Vector2.Distance(transform.position, closestTarget.position) < 1)
                {
                    hasGold = true;
                    closestTarget.GetComponent<GoldMine>().BeMined(gatheringRate);
                }
            }
            else
            {
                WalkTo(castle);

                if (Vector2.Distance(transform.position, castle.position) < 1)
                {
                    hasGold = false;
                }
            }
        }
    }
}
