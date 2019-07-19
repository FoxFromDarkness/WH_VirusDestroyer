using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public GameObject trapObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TrapCollisionBehaviour(collision);
    }

    private void TrapCollisionBehaviour(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            trapObject.gameObject.SetActive(true);
        }
    }
}
