using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlackCristalUI : MonoBehaviour
{
    public TextMeshProUGUI BcText { get; set; }

    private void Start()
    {
        BcText = GetComponentInChildren<TextMeshProUGUI>();
    }
}
