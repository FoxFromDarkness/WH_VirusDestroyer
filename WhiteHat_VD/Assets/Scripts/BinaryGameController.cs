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
            transform.parent.gameObject.SetActive(false);
            PlayerController.LevelPortalController.IsActive = true;
        }
    }

    internal void StartGame()
    {
        this.transform.parent.gameObject.SetActive(true);
        ChooseNewNumber();
    }
}
