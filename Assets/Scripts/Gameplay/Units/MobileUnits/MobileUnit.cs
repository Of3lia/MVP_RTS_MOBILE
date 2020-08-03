using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUnit : SeekerUnit, IMobile
{
    [SerializeField]
    protected float speed = 0.1f;

    protected Animator animator;

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
}
