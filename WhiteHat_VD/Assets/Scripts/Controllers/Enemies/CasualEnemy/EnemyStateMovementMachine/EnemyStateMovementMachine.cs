using UnityEngine;
using System.Collections;

public abstract class EnemyStateMovementMachine : MonoBehaviour
{
    protected EnemyStateMovement enemyStateMovement;

    public void SetEnemyState(EnemyStateMovement enemyStateMovement) => this.enemyStateMovement = enemyStateMovement;
    
}
