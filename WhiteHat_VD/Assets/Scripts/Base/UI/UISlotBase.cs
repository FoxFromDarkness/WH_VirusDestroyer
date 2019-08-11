using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlotBase : MonoBehaviour
{
    private Image[] ammoImage;

    private void Start()
    {
        ammoImage = GetComponentsInChildren<Image>();
  
        
    }

    public void SwapImage()
    {

    }
}
