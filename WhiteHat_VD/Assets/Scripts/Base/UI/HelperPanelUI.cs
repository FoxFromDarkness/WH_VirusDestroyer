using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelperPanelUI : MonoBehaviour
{
    public TextMeshProUGUI HelpText { get; set; }
    [SerializeField]
    public Image AmmoImageL;
    [SerializeField]
    public Image AmmoImageR;

    private void Start()
    {
        HelpText = GetComponentInChildren<TextMeshProUGUI>();

    }
}
