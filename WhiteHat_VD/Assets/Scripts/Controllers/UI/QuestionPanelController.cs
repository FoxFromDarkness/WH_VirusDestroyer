﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionPanelController : PanelBase
{
    [Header("QuestionPanelController")]

    private TextMeshProUGUI questionTitle;
    private Button ans_A;
    private Button ans_B;
    private Button ans_C;
    private Button ans_D;

    private static QuestionBase questionBase;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void QuestionBehaviour()
    {
        gameObject.SetActive(true);
        GameController.IsInputEnable = false;
        InitButtons();
        SetQuestion();
    }

    private void InitButtons()
    {
        questionTitle = GetComponentInChildren<TextMeshProUGUI>();

        Button[] answers = questionTitle.GetComponentsInChildren<Button>();

        ans_A = answers[0];
        ans_B = answers[1];
        ans_C = answers[2];
        ans_D = answers[3];
    }

    private void SetQuestion()
    {
        questionBase = QuestionController.RandQuestion();

        questionTitle.text = questionBase.question;
        ans_A.GetComponentInChildren<TextMeshProUGUI>().text = questionBase.ansA;
        ans_B.GetComponentInChildren<TextMeshProUGUI>().text = questionBase.ansB;
        ans_C.GetComponentInChildren<TextMeshProUGUI>().text = questionBase.ansC;
        ans_D.GetComponentInChildren<TextMeshProUGUI>().text = questionBase.ansD;
    }

    public void CheckAnswer(int numberOfButton)
    {
        ChangeVisibility();
        GameController.IsInputEnable = true;
        
        PlayerController.Chest.QuestionStatus = numberOfButton == questionBase.correctAns ? QuestionStatus.CORRECT : QuestionStatus.INCORRECT;
    }
}
