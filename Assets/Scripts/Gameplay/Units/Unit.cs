using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    protected SharedComponents sharedComponents;

    private Slider hpSlider;
    private Text hpText;

    [SerializeField]
    int maxHealthPoints;
    int currentHealthPoints;

    private Light2D light2d;

    [SerializeField]
    protected float los = 3; // LINE OF SIGHT

    protected bool nonPlayerUnit;

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
        if (CurrentHealthPoints > dmg)
        {
            CurrentHealthPoints -= dmg;
            if (!hpSlider.gameObject.activeSelf)
            {
                hpSlider.gameObject.SetActive(true);
                hpText.gameObject.SetActive(true);
                Invoke("DesactivateHpSlider", 3);
            }
        }
        else
            Die();
    }

    private void DesactivateHpSlider()
    {
        hpSlider.gameObject.SetActive(false);
        hpText.gameObject.SetActive(false);
    }

    private void Die()
    {
        light2d.pointLightInnerRadius = 0;
        Destroy(this.gameObject, 0.5f);
    }

    protected virtual void InitializeUnit()
    {
        sharedComponents = GetComponent<SharedComponents>();
        hpSlider = GetComponent<SharedComponents>().hpSlider;
        hpText = GetComponent<SharedComponents>().hpText;
        CurrentHealthPoints = MaxHealthPoints;
        light2d = GetComponent<Light2D>();
        if (!CompareTag(GENERAL.PLAYER.ToString())) 
        {
            nonPlayerUnit = true;
        }

        if (nonPlayerUnit)
        {
            light2d.enabled = false; hpText.gameObject.SetActive(false); hpSlider.gameObject.SetActive(false);
        }
        else
        {
            Invoke("DesactivateHpSlider", 3);
        }
        light2d.pointLightInnerRadius = los - 0.5f;
        light2d.pointLightOuterRadius = los;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GetComponent<MobileUnit>() != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, collision.transform.position, -0.01f);
        }
    }
}
