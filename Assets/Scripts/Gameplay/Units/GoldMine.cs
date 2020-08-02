using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldMine : MonoBehaviour
{
    private Slider hpSlider;
    private Text hpText;
    private SharedComponents sharedComponents;
    private int remainingGold = 200;

    private void Awake()
    {
        sharedComponents = GetComponent<SharedComponents>();
        InitializeUnit();   
    }

    private void InitializeUnit()
    {
        hpSlider = sharedComponents.hpSlider;

        hpSlider.maxValue = remainingGold;
        hpSlider.value = remainingGold;
        hpText = sharedComponents.hpText;
        hpText.text = remainingGold.ToString();
    }

    public void BeMined(int gatheringRate)
    {
        if(hpSlider.value > gatheringRate)
        {
            remainingGold-= gatheringRate;
            hpSlider.value= remainingGold;
            hpText.text = remainingGold.ToString();
        }
        else
        {
            Destroy(this.gameObject, 0.1f);
        }
    }
}

