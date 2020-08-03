using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingMobileUnit : MobileUnit
{
    [SerializeField]
    protected int attackPoints = 3;

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
            enemies = sharedComponents.p1Units;
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
        /*  if ((StepCounter.currentStep + creationFrame) % AtackDelay == 0)
          {
              closestTarget = GetClosest(enemies);
          }
          if (closestTarget != null)
          {
              if (Vector2.Distance(transform.position, closestTarget.position) <= 1)
              {
                  if (StepCounter.currentStep % 10 == 0)
                  {
                      Attack();
                  }
              }
              else
              {
                  WalkTo(closestTarget);
              }
          }
        */


        if (!attack)
        {
            if (closestTarget != null)
            {
                if (Vector2.Distance(transform.position, closestTarget.position) > 1)
                {
                    WalkTo(closestTarget);
                }
                else
                {
                    attack = true;
                }
            }
            else
            {
                WalkForward();
            }
        }
    }
    
    private void WalkForward()
    {
        transform.Translate(new Vector2(0, speed));
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
                    if (Vector2.Distance(transform.position, closestTarget.position) <= 1)
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
