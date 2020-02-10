using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleArea : MonoBehaviour
{
    private CasualEnemyController enemyController;
    [SerializeField] private bool isRightArea;

    private void Start()
    {
        enemyController = GetComponentInParent<CasualEnemyController>();
    }

    public void EnemyAreaTrigger(bool isTargetEnter)
    {
        enemyController.AreaTrigger(isTargetEnter, isRightArea);
    }
}
