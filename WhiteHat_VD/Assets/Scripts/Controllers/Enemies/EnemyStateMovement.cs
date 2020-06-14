using UnityEngine;
using System.Collections;

public abstract class EnemyStateMovement
{
    protected CasualEnemyController enemyController;

    protected EnemyStateMovement(CasualEnemyController enemyController)
    {
        this.enemyController = enemyController;
    }

    public virtual void StartStateMovement() { }

}
