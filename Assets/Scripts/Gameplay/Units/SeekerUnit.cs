using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerUnit : Unit, ISeek
{
    protected Transform closestTarget;
    public Transform GetClosest(Transform units_container)
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
            if (Vector2.Distance(transform.position, closestTarget.position) < los)
            {
                return closestTarget;
            }
            else { closestTarget = null; }
        }
        return null;
    }
}
