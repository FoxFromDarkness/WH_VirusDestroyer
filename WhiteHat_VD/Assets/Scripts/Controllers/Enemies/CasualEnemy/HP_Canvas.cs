using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Canvas : MonoBehaviour
{
    [SerializeField] private Image foreground;
    [SerializeField] private Image background;

    public void SetHP_Canvas(float hp)
    {
        foreground.fillAmount = hp;
    }
}
