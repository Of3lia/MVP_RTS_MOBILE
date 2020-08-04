using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingMobileUnit : MobileUnit
{
    [SerializeField]
    protected int attackPoints = 3;

    private int direction = 1;
    private int mapLimit = 8;

    Transform enemies;

    WaitForSeconds attackReloadTime;

    [SerializeField]
    private int AtackDelay = 15;
    private int creationFrame;
    
    bool attack;

    WaitForFixedUpdate waitFrame;

    protected override void InitializeUnit()
    {
        base.InitializeUnit();

        if (CompareTag("1"))
            enemies = sharedComponents.p2Units;
        else if (CompareTag("2"))
        {
            enemies = sharedComponents.p1Units;
            direction = -1;
            mapLimit = -8;
        }
        else
            Debug.LogWarning("No tag assigned");

        creationFrame = StepCounter.currentStep;
        attackReloadTime = new WaitForSeconds(0.5f);
        waitFrame = new WaitForFixedUpdate();
        StartCoroutine(AttackSystem());
    }

    private void FixedUpdate()
    {
        StateMachine();
    }

    protected override void StateMachine()
    {
        if (!attack)
        {
            if (closestTarget != null)
            {
                if (Vector2.Distance(transform.position, closestTarget.GetComponent<Collider2D>().ClosestPoint(transform.position)) > 0.5f)
                    WalkTo(closestTarget);
                else
                    attack = true;
            }
            else
                WalkForward();
        }
        else if(closestTarget == null)
        {
            attack = false;
        }
    }

    private void WalkForward()
    {
        if (transform.position.y * direction <= mapLimit * direction)
            transform.Translate(new Vector2(0, speed * direction));
        else
        {
            if(transform.position.x > 0)
            {   // Go to the left
                transform.Translate(new Vector2(-speed, 0));
            }
            else
            {   // Go to the right
                transform.Translate(new Vector2(speed, 0));
            }
        }
    }
    public override void WalkTo(Transform target)
    {
        base.WalkTo(target);
        
        /*if (!animator.GetBool("walk"))
        {
            animator.SetBool("walk", true);
            animator.SetBool("fight", false);
        }
        */
    }
    
    private IEnumerator AttackSystem()
    {
        while (true)
        {
            GetClosest(enemies);

            while (attack)
            {
                if (closestTarget != null)
                {
                    if (Vector2.Distance(transform.position, closestTarget.GetComponent<Collider2D>().ClosestPoint(transform.position)) <= 0.5f)
                    {
                        yield return attackReloadTime; // Attack Delay to start
                        MakeDamage();
                        yield return attackReloadTime; // Attack Delay to reload
                    }
                    else
                    {
                        attack = false;
                    }
                }
                else
                {
                    break;
                }
            }
            yield return waitFrame;
            yield return waitFrame;
            yield return waitFrame;
            yield return waitFrame;
        }
    }

    private void MakeDamage()
    {
        if (closestTarget != null)
        {
            closestTarget.GetComponent<Unit>().TakeDamage(attackPoints);
        }
    }
}
