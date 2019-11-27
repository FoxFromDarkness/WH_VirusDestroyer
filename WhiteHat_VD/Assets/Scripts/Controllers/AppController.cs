using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
    [SerializeField] private StartPanelController startPanel;

    public static AppController Instance;

    private void Start()
    {
        if (Instance == null)
            Instance = FindObjectOfType<AppController>();
    }

    public void ShowHideMainMenu()
    {
        startPanel.CloseSubPanels();
        startPanel.ChangeVisibility();
    }

    public void ExitApplication()
    {
        Debug.Log("Application Exit");
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ShowHideMainMenu();
        }
    }
}
