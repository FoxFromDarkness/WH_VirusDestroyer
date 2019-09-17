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
        hpBossText.text = "100%";
        if (backHPSliderColor != null)
            hpBackImg.color = backHPSliderColor;
        if (backHPSliderColor != null)
            hpFrontImg.color = frontHPSliderColor;
        this.gameObject.SetActive(true);
    }

    public void ActualHpBossSlider(float actualBossHP)
    {
        throw new System.Exception();
    }
}
