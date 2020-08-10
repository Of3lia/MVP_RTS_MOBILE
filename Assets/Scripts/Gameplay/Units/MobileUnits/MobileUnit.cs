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

    private Vector2 nextPos;

    public virtual void WalkTo(Transform target)
    {
        nextPos = Vector2.MoveTowards(transform.position, target.position, speed);
        
        transform.position = new Vector2(Mathf.Round(nextPos.x * 10) / 10, Mathf.Round(nextPos.y * 10) / 10);

        targetPos = target.position;
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        spriteContainer.transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 90);
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
    }
}
