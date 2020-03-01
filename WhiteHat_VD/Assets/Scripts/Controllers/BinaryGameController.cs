using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BinaryGameController : MonoBehaviour
{
    [SerializeField]
    private Text targetNumberText;
    [SerializeField]
    private BinaryGameCalculator calculator;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private GameObject toggles;
    private int targetNumber;

    private void ChooseNewNumber()
    {
        targetNumber = UnityEngine.Random.Range(1, 255);
        targetNumberText.text = targetNumber.ToString();
    }

    private void Update()
    {
        if (calculator.Total == targetNumber)
        {
            targetNumber = -1;
            var allToggles = toggles.GetComponentsInChildren<BinaryGameToggleBase>();
            foreach (var toggle in allToggles)
            {
                toggle.GetComponent<Toggle>().isOn = false;
            }
            transform.parent.gameObject.SetActive(false);
            GameController.IsInputEnable = true;
            HeadPanelController.Instance.PlayUISFX(true);

            HeadPanelController.Instance.uiPanel.ShowHelperPanel("Press 'Up arrow' to enter", 0f);
            PlayerController.LevelPortalController.IsActive = true;
        }
    }

    internal void StartGame()
    {
        this.transform.parent.gameObject.SetActive(true);
        GameController.IsInputEnable = false;
        ChooseNewNumber();
    }
}
