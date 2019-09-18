using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBossUI : MonoBehaviour
{
    private Slider hpBossSlider;
    private Text hpBossText;
    private Image hpBackImg;
    private Image hpFrontImg;

    private float bossHP_Max;


    private void Awake()
    {
        hpBossSlider = GetComponent<Slider>();
        hpBossText = GetComponentInChildren<Text>();
        hpBackImg = GetComponentsInChildren<Image>()[0];
        hpFrontImg = GetComponentsInChildren<Image>()[1];
    }

    public void InitHpBossSlider(float bossHP, Color backHPSliderColor, Color frontHPSliderColor)
    {
        bossHP_Max = bossHP;
        hpBossSlider.value = hpBossSlider.maxValue;
        hpBossText.text = "100%";
        if (backHPSliderColor != default)
            hpBackImg.color = backHPSliderColor;
        if (backHPSliderColor != default)
            hpFrontImg.color = frontHPSliderColor;
        this.gameObject.SetActive(true);
    }

    public void ActualHpBossSlider(float actualBossHP)
    {
        float value = (actualBossHP / bossHP_Max) * hpBossSlider.maxValue;
        hpBossSlider.value = value;
        hpBossText.text = decimal.Round(decimal.Parse(value + ""), 1) + "%";
    }

    //public void ActualHpBossSlider(float actualSingleBossHP) //For single Boss1Behaviour
    //{
    //    float actualBossHP = bossHP_Max - actualSingleBossHP;
    //    float value = (actualBossHP / bossHP_Max) * hpBossSlider.maxValue;
    //    hpBossSlider.value = value;
    //    hpBossText.text = decimal.Round(decimal.Parse(value + ""), 1) + "%";
    //}
}
