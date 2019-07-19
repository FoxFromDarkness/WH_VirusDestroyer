﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBase : MonoBehaviour
{
    public void ChangeVisibility()
    {
        this.gameObject.SetActive(!(this.gameObject.activeSelf));
    }

    public void SetVisibility(bool isVisible)
    {
        this.gameObject.SetActive(isVisible);
    }
}
