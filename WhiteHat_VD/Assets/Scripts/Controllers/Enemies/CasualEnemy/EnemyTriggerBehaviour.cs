using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerBehaviour : MonoBehaviour
{
    private CasualEnemyController enemyController;

    private void Start()
    {
        enemyController = GetComponentInParent<CasualEnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyBorder>())
            enemyController.BorderCollision();
    }
}
