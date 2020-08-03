using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    protected SharedComponents sharedComponents;

    private Slider hpSlider;
    private Text hpText;

    [SerializeField]
    int maxHealthPoints;
    int currentHealthPoints;

    [SerializeField]
    private int CurrentHealthPoints 
    {
        get => currentHealthPoints;
        set { currentHealthPoints = value; hpSlider.value = value; hpText.text = value.ToString(); }
    }
    [SerializeField]
    private int MaxHealthPoints
    {
        get => maxHealthPoints; 
        set { maxHealthPoints = value; hpSlider.maxValue = value; hpText.text = value.ToString(); }
    }

    protected virtual void Start()
    {
        InitializeUnit();
    }

    protected virtual void StateMachine() { }
    
    public void TakeDamage(int dmg)
    {
        if(CurrentHealthPoints > dmg)
            CurrentHealthPoints -= dmg;
        else
            Die();
    }

    private void Die()
    {
        Destroy(this.gameObject, 0.1f);
    }

    protected virtual void InitializeUnit()
    {
        sharedComponents = GetComponent<SharedComponents>();
        hpSlider = GetComponent<SharedComponents>().hpSlider;
        hpText = GetComponent<SharedComponents>().hpText;
        CurrentHealthPoints = MaxHealthPoints;
    }

    /*
    protected SharedComponents sharedComponents;

    private Slider hpSlider;
    private Text hpText;

    [SerializeField]
    private int healthPoints = 1;
    [SerializeField]
    private int attackPoints = 1;
    [SerializeField]
    protected float
        range = 1;
    [SerializeField]
    private float attackSpeed = 0.5f;

    private Transform enemyUnits;

    protected Transform closestEnemy;

    protected bool isAttacking = false;

    private void Awake()
    {
        sharedComponents = GetComponent<SharedComponents>();
        InitializeUnit();
    }

    private void Start()
    {
        if (CompareTag("1")) { enemyUnits = sharedComponents.p2Units; }
        else if (CompareTag("2")) { enemyUnits = sharedComponents.p1Units; }
        else { Debug.LogWarning("No Team Tag Assigned"); }
    }

    private void InitializeUnit()
    {
        hpSlider = sharedComponents.hpSlider;
        hpText = sharedComponents.hpText;

        hpSlider.maxValue = healthPoints;
        hpSlider.value = healthPoints;

        hpText.text = healthPoints.ToString();
    }

    protected Transform GetClosestEnemy()
    {
        if (enemyUnits.childCount > 0)
        {
            if (closestEnemy == null)
            {
                closestEnemy = enemyUnits.GetChild(0);
            }
            for (int i = 0; i < enemyUnits.childCount; i++)
            {
                if (Vector2.Distance( transform.position, enemyUnits.GetChild(i).position) 
                                    < 
                    Vector2.Distance(transform.position, closestEnemy.position) )
                {
                    closestEnemy = enemyUnits.GetChild(i).transform;
                } 
            }
        }
        return closestEnemy;
    }

    protected void Attack()
    {
        isAttacking = true;
        InvokeRepeating("AttackRepeating",attackSpeed, attackSpeed);
    }

    private void AttackRepeating()
    {
        if (closestEnemy != null)
        {
            closestEnemy.GetComponent<Unit>().TakeDamage(attackPoints);
        }
        else
        {
            StopAttacking();
        }
    }

    protected void StopAttacking()
    {
        isAttacking = true;
        CancelInvoke("AttackRepeating");
    }

    public void TakeDamage(int attack)
    {
        if (healthPoints > attack)
        {
            healthPoints -= attack;
            hpSlider.value = healthPoints;
            hpText.text = healthPoints.ToString();
        }
        else
        {
            StopAttacking();
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject, 0.1f);
    }
    */
}
