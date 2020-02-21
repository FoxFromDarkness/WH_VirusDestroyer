using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BinaryGameCalculator : MonoBehaviour
{
    [SerializeField]
    private Text totalText;

    [SerializeField]
    private GameObject toggles;

    public int Total { get; private set; }

    private void Start()
    {
        var allToggles = toggles.GetComponentsInChildren<BinaryGameToggleBase>();
        foreach (var toggle in allToggles)
        {
            toggle.OnToggleChanged += Toggle_OnToggleChanged;
        }
    }

    private void Toggle_OnToggleChanged(int number, bool enabled)
    {
        if (enabled)
            Total += number;
        else
            Total -= number;

        totalText.text = Total.ToString();
    }
}
