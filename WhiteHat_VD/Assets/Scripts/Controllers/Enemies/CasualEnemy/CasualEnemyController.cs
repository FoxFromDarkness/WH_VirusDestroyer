using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasualEnemyController : MonoBehaviour
{
    public enum EnemyType
    {
        PATROL, //patroluje teren
        GUARD, //strzeze danego miejsca
        TOWER, //Wieża straznicza
        NONE
    }

    public enum EnemyMovement
    {
        IDLE, //Z dala od gracza
        ATTACK, //Blisko gracza
        DIE, //Po zabiciu przez gracza
        NONE
    }

    public enum EnemyAttack
    {
        SINGLE, //Pojedyńczy strzał
        REPEATING, //Powtarzalny strzał
        LASER, //Laserowa wiązka
        KAMIKADZE, //Biegnie w bohatera
        NONE
    }

    [SerializeField] private Transform body;
    private SpriteRenderer bodyImage;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private EnemyMovement enemyMovement = EnemyMovement.IDLE;
    [SerializeField] private EnemyAttack enemyAttack;
    [SerializeField] private float healthPoints = 100;
    [SerializeField] private float enemyMovingSpeed = 2;
    [SerializeField] private float enemyAttackSpeed = 2;
    [SerializeField] private bool movingRight = true;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canAttack = true;
    [SerializeField] private bool isActive = true;

    //
    private PlayerController target;
    private bool isTargetInArea;

    private void Start()
    {
        bodyImage = body.GetComponent<SpriteRenderer>();
        target = FindObjectOfType<PlayerController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

        switch (enemyMovement)
        {
            case EnemyMovement.IDLE:
                Idle();
                break;
            case EnemyMovement.ATTACK:
                Attack();
                break;
            case EnemyMovement.DIE:
                //wybuch
                break;
            case EnemyMovement.NONE:
                break;
            default:
                break;
        }
    }

    private void Attack()
    {
        if (!canAttack) return;

        switch (enemyAttack)
        {
            case EnemyAttack.SINGLE:
                break;
            case EnemyAttack.REPEATING:
                break;
            case EnemyAttack.LASER:
                break;
            case EnemyAttack.KAMIKADZE:
                break;
            case EnemyAttack.NONE:
                break;
            default:
                break;
        }
    }

    private void Idle()
    {
        if (!canMove) return;

        switch (enemyType)
        {
            case EnemyType.PATROL:
                    body.Translate(enemyMovingSpeed * Time.deltaTime * (movingRight == true ? 1 : -1), 0, 0);
                break;
            case EnemyType.GUARD:
                    body.Translate(enemyMovingSpeed * Time.deltaTime * (movingRight == true ? 1 : -1), 0, 0);
                break;
            case EnemyType.TOWER:
                break;
            case EnemyType.NONE:
                break;
            default:
                break;
        }
    }

    internal void BorderCollision()
    {
        switch (enemyType)
        {
            case EnemyType.PATROL:
                movingRight = !movingRight;
                bodyImage.flipX = !bodyImage.flipX; //flipX ==> false (movingRight = true)
                break;
            case EnemyType.GUARD:
                canMove = false;
                bodyImage.flipX = !bodyImage.flipX;
                break;
            case EnemyType.TOWER:
                break;
            case EnemyType.NONE:
                break;
            default:
                break;
        }
    }


    internal void AreaTrigger(bool isTargetEnter, bool isRightArea)
    {
        isTargetInArea = isTargetEnter;
        movingRight = isRightArea;
        bodyImage.flipX = !movingRight;

        if(isTargetInArea)
        {
            StopCoroutine(EnemyBackToIdle(0));
            enemyMovement = EnemyMovement.ATTACK;
        }
        else
        {
            StartCoroutine(EnemyBackToIdle(enemyAttackSpeed));
        }
    }

    private IEnumerator EnemyBackToIdle(float time)
    {
        yield return new WaitForSeconds(time);
        if (!isTargetInArea) enemyMovement = EnemyMovement.IDLE;
    }
}
