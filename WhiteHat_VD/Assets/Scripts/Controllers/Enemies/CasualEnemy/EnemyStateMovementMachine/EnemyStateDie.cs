using UnityEngine;

internal class EnemyStateDie : EnemyStateMovement
{
    public EnemyStateDie(CasualEnemyController enemyController) : base(enemyController)
    {
    }

    public override void StartStateMovement()
    {
        enemyController.canAttack = false;
        enemyController.canMove = false;
        enemyController.isActive = false;
        enemyController.bodyImage.enabled = false;
        Object.Destroy(enemyController.Body.GetComponent<Rigidbody2D>());
        enemyController.bodyCollider.enabled = false;
        enemyController.explosionEffect.SetActive(true);
        Object.Destroy(enemyController.explosionEffect, 4.0f);
        Object.Destroy(enemyController.gameObject, 7.0f);
    }
}