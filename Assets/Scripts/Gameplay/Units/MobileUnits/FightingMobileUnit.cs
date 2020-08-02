using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingMobileUnit : MobileUnit, IFighter
{
    [SerializeField]
    protected int attackPoints = 3;

    Transform enemies;

    protected override void InitializeUnit()
    {
        base.InitializeUnit();

        if (CompareTag("1"))
            enemies = sharedComponents.p2Units;
        else if (CompareTag("2"))
            enemies = sharedComponents.p1Units;
        else
            Debug.LogWarning("No tag assigned");
    }
    
    private void FixedUpdate()
    {
        StateMachine();
    }

    protected override void StateMachine()
    {
        if (Time.frameCount % 5 == 0)
        {
            closestTarget = GetClosest(enemies);
        }
        if (closestTarget != null)
        {
            if (Vector2.Distance(transform.position, closestTarget.position) <= 1)
            {
                Attack();
            }
            else
            {
                WalkTo(closestTarget);
            }
        }
    }

    public override void WalkTo(Transform target)
    {
        base.WalkTo(target);
        
        if (!animator.GetBool("walk"))
        {
            animator.SetBool("walk", true);
            animator.SetBool("fight", false);
        }
    }
    public void Attack()
    {
        if (closestTarget != null)
                animator.SetBool("fight", true);
                animator.SetBool("walk", false);
    }

    // This is called on the animator
    private void MakeDamage()
    {
        if (closestTarget != null)
        {
            closestTarget.GetComponent<Unit>().TakeDamage(attackPoints);
        }
    }
}
