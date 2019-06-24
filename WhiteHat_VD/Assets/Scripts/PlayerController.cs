using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PanelController panelController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision with " + collision.gameObject.tag);

        QuestionCollisionBehaviour(collision);

    }

    private void QuestionCollisionBehaviour(Collider2D collision)
    {
        if (collision.gameObject.tag == "Question")
        {
            if (!panelController.gameObject.activeSelf)
            {
                panelController.SetVisibility(true);
                panelController.SetQuestion();
                Destroy(collision.gameObject);
                this.gameObject.SetActive(false);
            }
        }
    }



}
