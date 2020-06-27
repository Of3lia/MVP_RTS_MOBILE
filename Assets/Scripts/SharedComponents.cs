using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharedComponents : MonoBehaviour
{
    public Slider hpSlider;
    public Text hpText;

    public Transform p1Units;
    public Transform p2Units;

    private void Awake()
    {
        p1Units = GameObject.Find("P1_Units").transform;
        p2Units = GameObject.Find("P2_Units").transform;
    }
}
