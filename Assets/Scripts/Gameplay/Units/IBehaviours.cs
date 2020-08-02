using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IFighter
{
    void Attack();
}

interface IMobile
{
    void WalkTo(Transform target);
}

interface ISeeker
{
    Transform GetClosest(Transform units_container, float LOS);
}
