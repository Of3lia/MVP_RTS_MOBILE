using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Knight : FightingMobileUnit
{
    protected override void InitializeUnit()
    {
        base.InitializeUnit();

        if (CompareTag("1"))
        {
            poolParent = GameObject.Find("P1_Knight_Pool").transform;
        }
        else if (CompareTag("2"))
        {
            poolParent = GameObject.Find("P1_Knight_Pool").transform;
        }
        else
        {
            Debug.LogWarning("No tag assigned");
        }

        radius = 1.0f;
    }
}
