using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlackCristalUI : MonoBehaviour
{
    [HideInInspector]
    public TextMeshProUGUI bcText { get; set; }

    private void Start()
    {
        bcText = GetComponentInChildren<TextMeshProUGUI>();
    }
}
