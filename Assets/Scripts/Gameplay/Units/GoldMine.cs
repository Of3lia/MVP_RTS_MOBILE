using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldMine : MonoBehaviour
{
    private SharedComponents sharedComponents;
    private Slider hpSlider;
    private Text hpText;

    private int remainingGold = 600;

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

    public void BeMined(int gatheringRate, int tag)
    {
        if (tag == GENERAL.PLAYER) { ShowResourcesLeft(); }
        if(hpSlider.value > gatheringRate)
        {
            remainingGold-= gatheringRate;
            hpSlider.value= remainingGold;
            hpText.text = remainingGold.ToString();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void ShowResourcesLeft()
    {
        //CancelInvoke("HideResourcesLeft");
        hpSlider.gameObject.SetActive(true);
        hpText.gameObject.SetActive(true);
        //Invoke("HideResourcesLeft", 1);
    }

    private void HideResourcesLeft()
    {
        hpSlider.gameObject.SetActive(false);
        hpText.gameObject.SetActive(false);
    }
}

