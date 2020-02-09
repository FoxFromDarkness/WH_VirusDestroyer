using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPortalController : MonoBehaviour
{
    [SerializeField]
    private bool isActive = true;
    public bool IsActive { get { return isActive; } set { isActive = value; SetActive(value); } }
    public string descriptionOpen = "Press 'Up arrow' to enter";
    public string descriptionClosed = "Press 'Up arrow' to unlock portal";
    public Vector2 startLevelPosition;
    public string thisSceneName;
    public string nextSceneName;

    private void Start()
    {
        SetActive(isActive);
    }

    private void SetActive(bool value)
    {
        GetComponent<SpriteRenderer>().enabled = value;
    }
}
