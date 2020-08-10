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

    int attackReloadTimeInFrames = 5;

    bool attack;

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

        //creationFrame = StepCounter.currentStep;

        StartCoroutine(AttackSystem());
    }

    protected override IEnumerator StateMachine()
    {
        while (true)
        {
            while (this.enabled && GameMenu.GAME_STARTED)
            {
                if (enemyCastle != null)
                {
                    if (!attack)
                    {
                        if (closestTarget != null)
                        {
                            if ((Mathf.Round(Vector2.Distance(transform.position, closestTarget.position) * 10) / 10) > radius + closestTarget.GetComponent<Unit>().radius)
                                WalkTo(closestTarget);
                            else
                                attack = true;
                        }
                        else
                            WalkForward();
                    }
                    else if (closestTarget == null || !closestTarget.gameObject.activeSelf)
                    {
                        attack = false;
                    }

                }
                yield return waitFrame;
            }
            yield return waitFrame;
        }
    }

    private void WalkForward()
    {

        WalkTo(enemyCastle);
        
        ////if (transform.position.y /* * direction */ <= mapLimit /* * direction*/)
        //if (transform.localPosition.y * direction <= mapLimit * direction)
        //{
        //    transform.Translate(new Vector2(0, speed /* * direction */));
        //    spriteContainer.transform.localRotation = Quaternion.Euler(0, 0, 0);
        //}
        //else
        //{
        //    if (transform.position.x > 0)
        //    {   // Go to the left
        //        transform.Translate(new Vector2(-speed * direction, 0));
        //    }
        //    else
        //    {   // Go to the right
        //        transform.Translate(new Vector2(speed * direction, 0));
        //    }
        //}
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
            while (this.enabled && GameMenu.GAME_STARTED)
            {
                GetClosest(enemies);

                while (attack)
                {
                    if (closestTarget != null)
                    {
                        if ((Mathf.Round(Vector2.Distance(transform.position, closestTarget.position) * 10) / 10) <= radius + closestTarget.GetComponent<Unit>().radius)
                        {
                            int lastStep = StepCounter.currentStep;

                            //while (lastStep > StepCounter.currentStep - attackReloadTimeInFrames)
                            //{
                            yield return waitFrame;
                            yield return waitFrame;
                            yield return waitFrame;
                            yield return waitFrame;
                            yield return waitFrame;
                            ////}
                            MakeDamage();
                            yield return waitFrame;
                            yield return waitFrame;
                            yield return waitFrame;
                            yield return waitFrame;
                            yield return waitFrame;
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
            }
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
