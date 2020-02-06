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
    [SerializeField] private Collider2D bodyCollider;
    private HP_Canvas hpCanvas;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private EnemyMovement enemyMovement = EnemyMovement.IDLE;
    [SerializeField] private EnemyAttack enemyAttack;
    [SerializeField] private float maxHealthPoints = 100;
    private float currentHealthPoints;
    [SerializeField] private float enemyMovingSpeed = 2;
    [SerializeField] private float enemyAttackSpeed = 2;
    [SerializeField] private bool movingRight = true;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canAttack = true;
    [SerializeField] private bool isActive = true;
    [SerializeField] private GameObject explosionEffect;

    //
    private PlayerController target;
    private bool isTargetInArea;

    private void Start()
    {
        currentHealthPoints = maxHealthPoints;

        bodyImage = body.GetComponent<SpriteRenderer>();

        hpCanvas = body.GetComponentInChildren<HP_Canvas>();
        hpCanvas.gameObject.SetActive(false); 

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
                Die();
                break;
            case EnemyMovement.NONE:
                break;
            default:
                break;
        }
    }

    private void Die()
    {
        canAttack = false;
        canMove = false;
        isActive = false;
        bodyImage.enabled = false;
        Destroy(body.GetComponent<Rigidbody2D>());
        bodyCollider.enabled = false;
        explosionEffect.SetActive(true);
        Destroy(explosionEffect, 4.0f);
        Destroy(this.gameObject, 7.0f);

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

    public void GetDamage(float damage)
    {
        currentHealthPoints -= damage;
        hpCanvas.SetHP_Canvas(currentHealthPoints / maxHealthPoints);
        if(!hpCanvas.gameObject.activeSelf) hpCanvas.gameObject.SetActive(true);

        if (currentHealthPoints <= 0)
        {
            hpCanvas.gameObject.SetActive(false);
            enemyMovement = EnemyMovement.DIE;
        }
    }
}
