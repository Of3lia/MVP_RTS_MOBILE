﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : Building
{
    protected override void InitializeUnit()
    {
        base.InitializeUnit();

        if (CompareTag("1"))
        {
            poolParent = GameObject.Find("P1_Castle_Pool").transform;
        }
        else if (CompareTag("2"))
        {
            poolParent = GameObject.Find("P2_Castle_Pool").transform;
        }
        else
        {
            Debug.LogWarning("No tag assigned");
        }
    }
}
