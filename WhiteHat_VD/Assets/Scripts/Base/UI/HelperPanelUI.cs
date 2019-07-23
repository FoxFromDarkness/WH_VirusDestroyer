using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelperPanelUI : MonoBehaviour
{
    [HideInInspector]
    public TextMeshProUGUI helpText { get; set; }

    private void Start()
    {
        helpText = GetComponentInChildren<TextMeshProUGUI>();
    }
}
