using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUnit : SeekerUnit, IMobile
{
    [SerializeField]
    protected float speed = 0.1f;

    protected Animator animator;

    protected Transform spriteContainer;

    //protected RaycastHit2D hit;
    //protected ColliderDistance2D distance;
    private Vector2 targetPos;

    public virtual void WalkTo(Transform target)
    {
        if (target.GetComponent<Collider2D>())
        {
            transform.position = Vector2.MoveTowards(transform.position, Physics2D.ClosestPoint(transform.position, target.GetComponent<Collider2D>()), speed);

            targetPos = target.position;
            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x);
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotate Object
            spriteContainer.transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 90);
        }
    }

    protected override void InitializeUnit()
    {
        base.InitializeUnit();

        try
        {
            animator = GetComponent<Animator>();
        }
        catch
        {
            Debug.LogWarning("No animator on this object");
        }

        spriteContainer = sharedComponents.spriteContainer;

        StartCoroutine(CorrectPosition());
    }

    Transform closestAlly;
    private IEnumerator CorrectPosition()
    {
        while (this.enabled && GameMenu.GAME_STARTED)
        {
            transform.position = new Vector2(Mathf.Round(transform.position.x * 100) / 100, Mathf.Round(transform.position.y * 100) / 100);
            yield return new WaitForFixedUpdate();
        }
    }
    /*
    private IEnumerator AvoidUnits()
    {
        while (true)
        {
            hit = Physics2D.CircleCast(transform.position, 0.5f, transform.position);
            
            if (hit.collider != null)
            {
                if (hit.collider != GetComponent<Collider2D>())
                {
                    distance = Physics2D.Distance(GetComponent<Collider2D>(), hit.collider);

                    if (distance.isOverlapped)
                    {
                        if (hit.point.y == transform.position.y)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, hit.point, -0.05f);
                        }
                        else
                        {
                            transform.Translate(new Vector2(0.01f, 0));
                            transform.position = Vector2.MoveTowards(transform.position, hit.point, -0.05f);
                        }
                    }
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
    */
}
