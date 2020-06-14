using UnityEngine;
using System.Collections;

public class EnemyStateAttack : EnemyStateMovement
{
    public EnemyStateAttack(CasualEnemyController enemyController) : base(enemyController)
    {
    }

    public override void StartStateMovement()
    {
        if (!enemyController.canAttack) return;

        enemyController.currentEnemyAttackTime += Time.deltaTime;

        if (enemyController.enemyType == CasualEnemyController.EnemyType.TOWER) enemyController.PlayerFollowing();
        if (enemyController.currentEnemyAttackTime < enemyController.enemyAttackSpeed) return;

        enemyController.currentEnemyAttackTime = 0;
        enemyController.CurrentState = 0;
        enemyController.IsAim = true;

        switch (enemyController.enemyAttack)
        {
            case CasualEnemyController.EnemyAttack.SINGLE:
                CreateEnemyBullet();
                break;
            case CasualEnemyController.EnemyAttack.REPEATING:
                enemyController.StartCoroutine(CoRepeatingAttack());
                break;
            case CasualEnemyController.EnemyAttack.LASER:
                enemyController.StartCoroutine(CoLaserAttack());
                break;
            case CasualEnemyController.EnemyAttack.KAMIKADZE:
                break;
            case CasualEnemyController.EnemyAttack.NONE:
                break;
            default:
                break;
        }
    }

    private void CreateEnemyBullet()
    {
        if (!GameController.IsInputEnable) return;

        var enemyBullet = GetEnemyBullet();
        var copyEnemyBullet = Object.Instantiate(enemyBullet, enemyBullet.transform.parent);
        copyEnemyBullet.transform.position = enemyBullet.transform.position;
        copyEnemyBullet.transform.rotation = enemyBullet.transform.rotation;
        copyEnemyBullet.isMoving = true;
        copyEnemyBullet.speed *= enemyController.movingRight == true ? 1 : -1;
        copyEnemyBullet.gameObject.SetActive(true);
        enemyController.animator?.Play("Shoot");
        enemyController.audioSource.Play();
    }

    private BulletEnemyController GetEnemyBullet()
    {
        if (enemyController.enemyRocketBullet == null)
        {
            switch (enemyController.enemyBody)
            {
                case CasualEnemyController.EnemyBody.BLACK:
                    return enemyController.enemyShotgunBullet;
                default:
                    return enemyController.enemyRiflegunBullet;
            }
        }
        else
            return enemyController.enemyRocketBullet;
    }

    private IEnumerator CoRepeatingAttack()
    {
        enemyController.canAttack = false;
        for (int i = 0; i < enemyController.amountBulletsInSingleSeries; i++)
        {
            CreateEnemyBullet();
            yield return new WaitForSeconds(enemyController.enemyAttackSpeed / enemyController.amountBulletsInSingleSeries);
        }
        enemyController.canAttack = true;
    }

    private IEnumerator CoLaserAttack()
    {
        enemyController.canAttack = false;
        enemyController.canMove = false;
        enemyController.enemyLaser.gameObject.SetActive(true);
        yield return new WaitForSeconds(enemyController.enemyAttackSpeed);
        enemyController.enemyLaser.gameObject.SetActive(false);
        enemyController.canAttack = true;
        enemyController.canMove = true;
    }
}
