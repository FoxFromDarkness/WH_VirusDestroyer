using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public QuestionPanelController questionPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        QuestionDoorBehavior(collision);
    }

    private void QuestionDoorBehavior(Collider2D collision)
    {
        if (collision.GetComponent<QuestionDoorBase>())
        {
            if (!questionPanel.gameObject.activeSelf)
            {
                questionPanel.QuestionBehaviour();
                Destroy(collision.gameObject);
                this.gameObject.SetActive(false);
            }
        }
    }
}
