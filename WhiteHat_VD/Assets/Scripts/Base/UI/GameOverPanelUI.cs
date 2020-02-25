using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelUI : MonoBehaviour
{
    public TextMeshProUGUI result;
    public Image img;

    public void SetColor(byte r, byte g, byte b)
    {
        Color32 color = new Color32(r, g, b, 52);
        img.color = color;
    }
}
