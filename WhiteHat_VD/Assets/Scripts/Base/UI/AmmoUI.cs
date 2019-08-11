using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [HideInInspector]
    public TextMeshProUGUI ammoText { get; set; }

    private void Start()
    {
        ammoText = GetComponentInChildren<TextMeshProUGUI>();
    }
}
