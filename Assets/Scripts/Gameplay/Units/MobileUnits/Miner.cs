using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Miner : MobileUnit
{
    private Transform goldMines;
    private bool hasGold = false;
    private Transform castle;
    private int gatheringRate = 7;
    private int player;
    private float initialLos;
    protected override void InitializeUnit()
    {
        base.InitializeUnit();

        goldMines = GameObject.Find("GoldMines").transform;

        if (CompareTag("1"))
        {
            castle = GameObject.Find("P1_Castle").transform;
            player = 1;
            poolParent = GameObject.Find("P1_Miner_Pool").transform;
        }
        else if (CompareTag("2"))
        {
            castle = GameObject.Find("P2_Castle").transform;
            player = 2;
            poolParent = GameObject.Find("P2_Miner_Pool").transform;
        }
        else
        {
            Debug.LogWarning("No tag assigned");
        }
        initialLos = los;
    }

    private void FixedUpdate()
    {
        if (closestTarget == null)
        {
            closestTarget = GetClosest(goldMines);
            los += 1;
        }
        else
            los = initialLos;
        
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

                if (Vector2.Distance(transform.position, closestTarget.position) < 0.5f)
                {
                    hasGold = true;
                    closestTarget.GetComponent<GoldMine>().BeMined(gatheringRate, int.Parse(tag));
                }
            }
            else
            {
                WalkTo(castle);

                if (Vector2.Distance(transform.position, castle.position) < 1f)
                {
                    hasGold = false;
                    if (player == GENERAL.PLAYER)
                    {
                        GENERAL.GOLD += gatheringRate;
                    }
                }
            }
        }
    }
}
