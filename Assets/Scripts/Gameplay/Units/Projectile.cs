using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 0.5f;
    private int attackPoints;
    private Transform target;
    private Unit targetUnit;
    private Vector2 targetPos;
    
    public void InitializeProjectile(Transform target, int attackPoints)
    {
        targetPos = target.position;
        this.attackPoints = attackPoints;
        targetUnit = target.GetComponent<Unit>();

        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 90);
    }
    private void FixedUpdate()
    {
        MachineState();
    }

    public void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, projectileSpeed);
    }

    private void MachineState()
    {
        if (Vector2.Distance(transform.position, targetPos) > 0.5f)
        {
            MoveToTarget();
        }
        else
        {
            if (targetUnit != null)
            {
                targetUnit.TakeDamage(attackPoints);
            }
            transform.localPosition = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
