using UnityEngine;
using System.Collections;

public class EnemyStateIdle : EnemyStateMovement
{
    public EnemyStateIdle(CasualEnemyController enemyController) : base(enemyController)
    {
    }

    public override void StartStateMovement()
    {
        if (!enemyController.canMove) return;

        enemyController.CurrentState = 1;
        enemyController.IsAim = false;
        switch (enemyController.enemyType)
        {
            case CasualEnemyController.EnemyType.PATROL:
                enemyController.Body.Translate(enemyController.enemyMovingSpeed * Time.deltaTime * (enemyController.movingRight == true ? 1 : -1), 0, 0);
                break;
            case CasualEnemyController.EnemyType.GUARD:
                enemyController.Body.Translate(enemyController.enemyMovingSpeed * Time.deltaTime * (enemyController.movingRight == true ? 1 : -1), 0, 0);
                break;
            case CasualEnemyController.EnemyType.TOWER:
                enemyController.PlayerFollowing();
                break;
            case CasualEnemyController.EnemyType.NONE:
                break;
            default:
                break;
        }
    }
}
