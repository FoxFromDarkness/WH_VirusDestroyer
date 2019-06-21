using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public QuestionController questionController;
    public GameObject player;

    private QuestionBase questionBase;

    public Text question;
    public Button ans_A;
    public Button ans_B;
    public Button ans_C;
    public Button ans_D;

    public void ChangeVisibility()
    {
        this.gameObject.SetActive(!(this.gameObject.activeSelf));
        Debug.Log(this.gameObject.name + " is " + this.gameObject.activeSelf);
    }

    public void SetVisibility(bool isVisible)
    {
        this.gameObject.SetActive(isVisible);
    }

    public void SetQuestion()
    {
        questionBase = questionController.RandQuestion();

        question.text = questionBase.question;
        ans_A.GetComponentInChildren<Text>().text = questionBase.ansA;
        ans_B.GetComponentInChildren<Text>().text = questionBase.ansB;
        ans_C.GetComponentInChildren<Text>().text = questionBase.ansC;
        ans_D.GetComponentInChildren<Text>().text = questionBase.ansD;
    }

    public void CheckAnswer(int numberOfButton)
    {
        if (numberOfButton == questionBase.correctAns)
        {
            player.SetActive(true);
            ChangeVisibility();
        }
        else
        {
            SetQuestion();
        }
            
    }

}
