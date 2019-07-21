using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerBase playerStats;

    public QuestionPanelController questionPanel;

    private void Start()
    {
        playerStats = GetComponent<PlayerBase>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        QuestionDoorBehavior(collision);
        DeadZoneBehavior(collision);
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

    private void DeadZoneBehavior(Collider2D collision)
    {
        if (collision.GetComponent<DeadZoneBase>())
        {
            this.gameObject.transform.position = new Vector3(-1240.0f, 255.0f);
            print(playerStats.playerStartPosition);
        }
    }
}
