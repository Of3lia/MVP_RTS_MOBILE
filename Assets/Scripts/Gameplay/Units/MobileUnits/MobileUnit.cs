using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUnit : Unit, IMobile, ISeeker
{
    [SerializeField]
    protected float speed = 0.1f;

    protected Animator animator;

    protected Transform closestTarget;
    public virtual void WalkTo(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed);
    }

    public Transform GetClosest(Transform units_container, float LOS = 10 /* LOS "Line of sight" will be used in future */)
    {
        if (units_container.childCount > 0)
        {
            closestTarget = units_container.GetChild(0);

            for (int i = 0; i < units_container.childCount; i++)
            {
                if (Vector2.Distance(transform.position, units_container.GetChild(i).position)
                                    <
                    Vector2.Distance(transform.position, closestTarget.position))
                {
                    closestTarget = units_container.GetChild(i).transform;
                }
            }
            return closestTarget;
        }
        return null;
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

    private void Start()
    {
        InitializeUnit();
    }
}
