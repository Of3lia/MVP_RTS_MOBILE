using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : RangedUnit
{
    //[SerializeField]
    private float buildingRange = 5;
    //[SerializeField]
    private int attackPoints = 12;
    [SerializeField]
    Projectile projectile;

    private WaitForFixedUpdate fixUpd;

    private bool attack;

    private Transform enemies;

    protected override void InitializeUnit()
    {
        base.InitializeUnit();

        if (CompareTag("1"))
            enemies = sharedComponents.p2Units;
        else if (CompareTag("2"))
            enemies = sharedComponents.p1Units;
        else
            Debug.LogWarning("No tag assigned");

        fixUpd = new WaitForFixedUpdate();

        StartCoroutine(ShootProjectile());
    }

    private IEnumerator ShootProjectile()
    {
        while (true)
        {
            yield return fixUpd;
            yield return fixUpd;
            yield return fixUpd;
            yield return fixUpd;
            yield return fixUpd;
            yield return fixUpd;
            yield return fixUpd;
            yield return fixUpd;
            yield return fixUpd;
            yield return fixUpd;
            GetClosest(enemies);
            if (closestTarget != null)
            {
                if (!projectile.gameObject.activeSelf)
                {
                    if (Vector2.Distance(transform.position, closestTarget.position) < buildingRange)
                    {
                        projectile.gameObject.SetActive(true);
                        projectile.InitializeProjectile(closestTarget, attackPoints);
                    }
                }
            }
            yield return fixUpd;
            yield return fixUpd;
            yield return fixUpd;
            yield return fixUpd;
            yield return fixUpd;
        }
    }
}
