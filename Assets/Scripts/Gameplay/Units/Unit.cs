using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.UI;

public class Unit : SincronizableObject
{
    protected SharedComponents sharedComponents;

    private int unitId;

    public Slider hpSlider;
    public Text hpText;

    [SerializeField]
    int maxHealthPoints;
    int currentHealthPoints;

    protected Transform poolParent;

    protected Transform inGameParent;

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
    public int UnitId
    {
        get { return unitId; }
        set { unitId = value; }
    }
   /* private void Update()
    {
        if (this.enabled && this.GetComponent<Swordsman>())
        {
            Debug.Log(GetSyncData());
        }
    }
   */

    private void Awake()
    {
        if (CompareTag("1"))
        {
            inGameParent = GameObject.Find("P1_Units").transform;
        }
        else if (CompareTag("2"))
        {
            inGameParent = GameObject.Find("P2_Units").transform;
        }
    }

    protected virtual void Start()
    {
        InitializeUnit();
    }

    protected virtual void StateMachine() { }
    
    public virtual void TakeDamage(int dmg)
    {
        if (CurrentHealthPoints > dmg)
        {
            CurrentHealthPoints -= dmg;
            if (!hpSlider.gameObject.activeSelf)
            {
                hpSlider.gameObject.SetActive(true);
                hpText.gameObject.SetActive(true);
                CancelInvoke("DesactivateHpSlider");
                Invoke("DesactivateHpSlider", 3f);
            }
        }
        else
            DesactivateUnit();
    }

    private void DesactivateHpSlider()
    {
        hpSlider.gameObject.SetActive(false);
        hpText.gameObject.SetActive(false);
    }

    protected virtual void InitializeUnit()
    {
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
            light2d.enabled = false; hpText.gameObject.SetActive(false); hpSlider.gameObject.SetActive(false);
        }
        else
        {
            Invoke("DesactivateHpSlider", 3);
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

    public void ActivateUnit(float posx, float posy)
    {
        this.enabled = true;
        currentHealthPoints = maxHealthPoints;
        transform.parent = inGameParent;
        transform.position = new Vector2(posx, posy);
        //Debug.Log(inGameParent);
    }
    private void DesactivateUnit()
    {
        transform.parent = poolParent;
        transform.localPosition = Vector2.zero;
        this.enabled = false;
    }

    public string GetSyncData()
    {
        //return $"id{unitId};posx{transform.position.x};posy{transform.position.y};rotationz{transform.rotation.z};hp{currentHealthPoints};";
        return ((float)Mathf.Round( transform.position.x * 100) / 100).ToString();
    }
}
