using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveInputPanelController : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button button;

    private void OnEnable()
    {
        inputField.text = "";
    }

    private void Start()
    {
        button.onClick.AddListener(() => OnButtonClick());
    }

    private void OnButtonClick()
    {
        if (inputField.text != "")
            SaveController.saveNickName = inputField.text;
        else
            SaveController.saveNickName = "Anonim";

        this.gameObject.SetActive(false);

        GameController.Instance.StartNewGame(false);
    }
}
