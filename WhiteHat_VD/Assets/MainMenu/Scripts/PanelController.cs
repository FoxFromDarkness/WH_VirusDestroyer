using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{

    public void ChangeVisibility()
    {
        this.gameObject.SetActive(!(this.gameObject.activeSelf));
        Debug.Log(this.gameObject.name + " is " + this.gameObject.activeSelf);
    }
}
