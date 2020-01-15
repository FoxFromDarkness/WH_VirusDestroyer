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
    private int targetNumber;

    private void ChooseNewNumber()
    {
        targetNumber = UnityEngine.Random.RandomRange(0, 255);
        targetNumberText.text = targetNumber.ToString();
    }

    private void Update()
    {
        if (calculator.Total == targetNumber)
        {
            ChooseNewNumber();
        }
    }
}
