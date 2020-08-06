using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUnit : SeekerUnit, IMobile
{
    [SerializeField]
    protected float speed = 0.1f;

    protected Animator animator;

    //protected RaycastHit2D hit;
    //protected ColliderDistance2D distance;

    public virtual void WalkTo(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed);
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
    }

    protected override void Start()
    {
        base.Start();

        //StartCoroutine(AvoidUnits());
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
