using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUnit : Unit
{
    [SerializeField]
    private float speed = 0.1f;

    private void FixedUpdate()
    {
        StateMachine();
    }

    protected void StateMachine()
    {
        GetClosestEnemy();

        if (closestEnemy != null) {
            if (Vector2.Distance(transform.position, closestEnemy.position) <= range)
            {
                if (isAttacking == false)
                {
                    Attack();
                }
            }
            else
            {
                StopAttacking();
                WalkToEnemy();
            }
        }
    }

    private void WalkToEnemy()
    {
        isAttacking = false;
        transform.position = Vector2.MoveTowards(transform.position, closestEnemy.position, speed);
    }
}
