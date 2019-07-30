using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);

        slider = GetComponentInChildren<Slider>();
    }

     public void SetSliderValue(float value)
    {
        slider.value = value;
    }
}
