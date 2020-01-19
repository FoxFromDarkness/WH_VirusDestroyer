﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBase : MonoBehaviour
{
    [SerializeField] private QuestionStatus questionStatus = QuestionStatus.DEFAULT;
    public QuestionStatus QuestionStatus { get { return questionStatus; } set { questionStatus = value;  CheckChestStatus(); } }

    [SerializeField]
    private Sprite questionChestSprite, correctChestSprite, incorrectChestSprite;

    [SerializeField]  private int awardAmount;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = questionChestSprite;
    }

    private void CheckChestStatus()
    {
        switch (questionStatus)
        {
            case QuestionStatus.DEFAULT:
                this.GetComponent<SpriteRenderer>().sprite = questionChestSprite;
                break;
            case QuestionStatus.CORRECT:
                this.GetComponent<SpriteRenderer>().sprite = correctChestSprite;
                StartCoroutine(OpenChest());
                break;
            case QuestionStatus.INCORRECT:
                this.GetComponent<SpriteRenderer>().sprite = incorrectChestSprite;
                break;
            default:
                break;
        }
    }

    private IEnumerator OpenChest()
    {
        yield return new WaitForSeconds(2.0f);

        Debug.Log("Animacja otwarcia skrzyni");
        Debug.Log("Wysyp " + awardAmount + " bitcoinów");
    }
}