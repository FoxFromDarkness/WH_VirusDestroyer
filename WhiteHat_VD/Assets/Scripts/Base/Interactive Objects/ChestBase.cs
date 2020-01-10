using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBase : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite questionChestSprite, correctChestSprite, incorrectChestSprite;

    [SerializeField]
    private bool isOpen;
    public bool IsOpen {
        get { return isOpen; }
        set { isOpen = value;
            if (answerIsCorrect)
                this.GetComponent<SpriteRenderer>().sprite = correctChestSprite;
            else
                this.GetComponent<SpriteRenderer>().sprite = incorrectChestSprite;
        }
    }

    [SerializeField]
    private bool answerIsCorrect;




    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = questionChestSprite;
    }

    // Update is called once per frame
    void Update()
    {
        switch (QuestionPanelController.questionStatus)
        {
            case QuestionStatus.DEFAULT:
                this.GetComponent<SpriteRenderer>().sprite = questionChestSprite;
                break;
            case QuestionStatus.CORRECT:
                this.GetComponent<SpriteRenderer>().sprite = correctChestSprite;
                break;
            case QuestionStatus.INCORRECT:
                this.GetComponent<SpriteRenderer>().sprite = incorrectChestSprite;
                break;
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
            IsOpen = !IsOpen;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            answerIsCorrect = !answerIsCorrect;

    }
}
