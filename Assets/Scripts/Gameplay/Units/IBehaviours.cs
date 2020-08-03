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

interface ISeek
{
    Transform GetClosest(Transform units_container);
}

interface IRanged
{
    void ShootProjectile(Vector2 targetPos);
}