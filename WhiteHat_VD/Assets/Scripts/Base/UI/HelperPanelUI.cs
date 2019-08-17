using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelperPanelUI : MonoBehaviour
{
    public TextMeshProUGUI HelpText { get; set; }

    private void Start()
    {
        HelpText = GetComponentInChildren<TextMeshProUGUI>();
    }
}
