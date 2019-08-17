using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlotBase : MonoBehaviour
{
    [SerializeField]
    private bool isSlot0;
    public bool IsSlot0 { get { return isSlot0; } }
    [SerializeField]
    private Image ammoImage;
    public Sprite AmmoImage { get { return ammoImage.sprite;  } set { ammoImage.sprite = value;  } }
}
