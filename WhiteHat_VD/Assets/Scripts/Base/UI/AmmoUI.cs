using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    public TextMeshProUGUI AmmoText { get; set; }

    private void Start()
    {
        AmmoText = GetComponentInChildren<TextMeshProUGUI>();
    }
}
