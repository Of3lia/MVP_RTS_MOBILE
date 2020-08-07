using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    protected SharedComponents sharedComponents;

    public static int classId;
    private int unitId;

    public Slider hpSlider;
    public Text hpText;

    [SerializeField]
    int maxHealthPoints;
    int currentHealthPoints;

    protected Transform poolParent;

    protected Transform enemyCastle;

    private UnityEngine.Experimental.Rendering.Universal.Light2D light2d;

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
                //CancelInvoke("DesactivateHpSlider");
                //Invoke("DesactivateHpSlider", 3f);
            }
        }
        else
            Destroy(this.gameObject);
    }

    private void Die()
    {
        transform.parent = poolParent;
        transform.position = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void DesactivateHpSlider()
    {
        hpSlider.gameObject.SetActive(false);
        hpText.gameObject.SetActive(false);
    }

    protected virtual void InitializeUnit()
    {
        unitId = classId;
        classId++;

        sharedComponents = GetComponent<SharedComponents>();
        hpSlider = GetComponent<SharedComponents>().hpSlider;
        hpText = GetComponent<SharedComponents>().hpText;
        CurrentHealthPoints = MaxHealthPoints;
        light2d = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        if (!CompareTag(GENERAL.PLAYER.ToString())) 
        {
            nonPlayerUnit = true;
        }

        if (nonPlayerUnit)
        {
            light2d.enabled = false; //hpText.gameObject.SetActive(false); hpSlider.gameObject.SetActive(false);
        }
        else
        {
            //Invoke("DesactivateHpSlider", 3);
        }
        light2d.pointLightInnerRadius = los - 0.5f;
        light2d.pointLightOuterRadius = los;

        if (CompareTag("1"))
        {
            enemyCastle = GameObject.Find("P2_Castle").transform;
        }
        else if (CompareTag("2"))
        {
            enemyCastle = GameObject.Find("P1_Castle").transform;
        }
    }

    public (int unitId, float posx, float posy, float rotationZ) GetSyncData()
    {
        return (unitId, transform.position.x, transform.position.y, transform.rotation.z);
    }
}
