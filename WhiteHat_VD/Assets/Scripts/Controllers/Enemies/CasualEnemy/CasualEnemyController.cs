using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasualEnemyController : MonoBehaviour
{
    public enum EnemyBody
    {
        BLACK,
        RED,
        DEFAULT
    }

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

    [Header("General")]
    [SerializeField] private Transform body;
    [SerializeField] private Sprite[] bodyImages;
    private SpriteRenderer bodyImage;
    [SerializeField] private Collider2D bodyCollider;
    public RuntimeAnimatorController[] animators;
    private Animator animator;
    private HP_Canvas hpCanvas;
    [Space]
    [Header("Type")]
    [SerializeField] private EnemyBody enemyBody;
    [SerializeField] private EnemyType enemyType;
    [Tooltip("Only for information")]
    [SerializeField] private EnemyMovement enemyMovement = EnemyMovement.IDLE;
    [SerializeField] private EnemyAttack enemyAttack;
    [Space]
    [Header("Settings")]
    [SerializeField] private float maxHealthPoints = 100;
    private float currentHealthPoints;
    [SerializeField] private float enemyMovingSpeed = 2;
    [SerializeField] private float enemyAttackSpeed = 2;
    [Tooltip("Only for 'Repeating Attack'")]
    [SerializeField] private int amountBulletsInSingleSeries = 3; //Ilość pocisków w serii (Repeating Atack)
    private float currentEnemyAttackTime = 0;
    [Space]
    [Header("Permissions")]
    [SerializeField] private bool movingRight = true;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canAttack = true;
    [SerializeField] private bool isActive = true;
    [Space]
    [Header("Animator")]
    [SerializeField] private bool _isAim;
    public bool IsAim { get { return _isAim; } set { _isAim = value; SetAnimator(_isAim, _currentState); } }
    [SerializeField] private int _currentState;
    public int CurrentState { get { return _currentState; } set { _currentState = value; SetAnimator(_isAim, _currentState); } }
    [Space]
    [Header("Weapons")]
    [SerializeField] private BulletEnemyController enemyBullet;
    [SerializeField] private BulletEnemyController enemyLaser;
    private Vector2 enemyLaserStartPos;
    private Vector2 enemyHPStartPos;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject damageEffect;

    //
    private PlayerController target;
    private bool isTargetInArea;
    //PlayerFollowing
    private Vector2 direction;
    private float angle;
    private Quaternion rotation;

    private void Start()
    {
        currentHealthPoints = maxHealthPoints;

        bodyImage = body.GetComponent<SpriteRenderer>();
        animator = body.GetComponent<Animator>();

        hpCanvas = body.GetComponentInChildren<HP_Canvas>();
        hpCanvas.gameObject.SetActive(false);

        enemyLaserStartPos = enemyLaser.transform.localPosition;
        enemyHPStartPos = hpCanvas.transform.localPosition;

        target = FindObjectOfType<PlayerController>();

        if (enemyType == EnemyType.TOWER) return;

        switch (enemyBody)
        {
            case EnemyBody.BLACK:
                bodyImage.sprite = bodyImages[0];
                animator.runtimeAnimatorController = animators[0];
                break;
            case EnemyBody.RED:
                bodyImage.sprite = bodyImages[1];
                animator.runtimeAnimatorController = animators[1];
                break;
            default:
                bodyImage.sprite = bodyImages[2];
                animator.runtimeAnimatorController = animators[2];
                break;
        }

        IsAim = _isAim;
        CurrentState = _currentState;
    }

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

    private void Idle()
    {
        if (!canMove) return;
        CurrentState = 1;
        IsAim = false;
        switch (enemyType)
        {
            case EnemyType.PATROL:
                    body.Translate(enemyMovingSpeed * Time.deltaTime * (movingRight == true ? 1 : -1), 0, 0);
                break;
            case EnemyType.GUARD:
                    body.Translate(enemyMovingSpeed * Time.deltaTime * (movingRight == true ? 1 : -1), 0, 0);
                break;
            case EnemyType.TOWER:
                    PlayerFollowing();
                break;
            case EnemyType.NONE:
                break;
            default:
                break;
        }
    }

    private void Attack()
    {
        if (!canAttack) return;

        currentEnemyAttackTime += Time.deltaTime;

        if (enemyType == EnemyType.TOWER) PlayerFollowing();
        if (currentEnemyAttackTime < enemyAttackSpeed) return;

        currentEnemyAttackTime = 0;
        CurrentState = 0;
        IsAim = true;

        switch (enemyAttack)
        {
            case EnemyAttack.SINGLE:
                CreateEnemyBullet();
                break;
            case EnemyAttack.REPEATING:
                StartCoroutine(CoRepeatingAttack());
                break;
            case EnemyAttack.LASER:
                StartCoroutine(CoLaserAttack());
                break;
            case EnemyAttack.KAMIKADZE:
                break;
            case EnemyAttack.NONE:
                break;
            default:
                break;
        }

    }

    private void CreateEnemyBullet()
    {
        var copyEnemyBullet = Instantiate(enemyBullet, enemyBullet.transform.parent);
        copyEnemyBullet.transform.position = enemyBullet.transform.position;
        copyEnemyBullet.transform.rotation = enemyBullet.transform.rotation;
        copyEnemyBullet.isMoving = true;
        copyEnemyBullet.speed *= movingRight == true ? 1 : -1;
        copyEnemyBullet.gameObject.SetActive(true);
        animator?.Play("Shoot");
    }

    private IEnumerator CoRepeatingAttack()
    {
        canAttack = false;
        for (int i = 0; i < amountBulletsInSingleSeries; i++)
        {
            CreateEnemyBullet();
            yield return new WaitForSeconds(enemyAttackSpeed / amountBulletsInSingleSeries);
        }
        canAttack = true;
    }

    private IEnumerator CoLaserAttack()
    {
        canAttack = false;
        canMove = false;
        enemyLaser.gameObject.SetActive(true);
        yield return new WaitForSeconds(enemyAttackSpeed);
        enemyLaser.gameObject.SetActive(false);
        canAttack = true;
        canMove = true;
    }

    private void CorrectLaserPosition()
    {
        if (enemyAttack == EnemyAttack.LASER)
        {
            enemyLaser.transform.localPosition = enemyLaserStartPos;
            enemyLaser.transform.localPosition = new Vector3(enemyLaser.transform.localPosition.x * (movingRight == true ? 1 : -1), enemyLaser.transform.localPosition.y, 0);
        }
    }

    private void PlayerFollowing()
    {
        Vector2 direction = -target.transform.position + body.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, enemyMovingSpeed * Time.deltaTime);
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

    public void GetDamage(float damage)
    {
        currentHealthPoints -= damage;
        hpCanvas.SetHP_Canvas(currentHealthPoints / maxHealthPoints);
        if (!hpCanvas.gameObject.activeSelf)
        {
            hpCanvas.gameObject.SetActive(true);
            hpCanvas.transform.localPosition = new Vector2(movingRight == true ? enemyHPStartPos.x : enemyHPStartPos.x * -1, hpCanvas.transform.localPosition.y);
        }

            if (currentHealthPoints <= 0)
        {
            hpCanvas.gameObject.SetActive(false);
            enemyMovement = EnemyMovement.DIE;
        }
        else
        {
            damageEffect.GetComponent<ParticleSystem>().Play();
        }
    }

    internal void BorderCollision()
    {
        switch (enemyType)
        {
            case EnemyType.PATROL:
                movingRight = !movingRight;
                bodyImage.flipX = !bodyImage.flipX; //flipX ==> false (movingRight = true)
                hpCanvas.transform.localPosition = new Vector2(movingRight == true ? enemyHPStartPos.x : enemyHPStartPos.x * -1, hpCanvas.transform.localPosition.y);
                CorrectLaserPosition();
                break;
            case EnemyType.GUARD:
                canMove = false;
                CurrentState = 0;
                bodyImage.flipX = !bodyImage.flipX;
                CorrectLaserPosition();
                hpCanvas.transform.localPosition = new Vector2(movingRight == true ? enemyHPStartPos.x : enemyHPStartPos.x * -1, hpCanvas.transform.localPosition.y);
                break;
            //case EnemyType.TOWER:
            //    break;
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
        hpCanvas.transform.localPosition = new Vector2(movingRight == true ? enemyHPStartPos.x : enemyHPStartPos.x * -1, hpCanvas.transform.localPosition.y);
        CorrectLaserPosition();


        if (isTargetInArea)
        {
            StopCoroutine(EnemyBackToIdle(0));
            enemyMovement = EnemyMovement.ATTACK;
            animator.Play("Idle_Aim");
        }
        else
        {
            StartCoroutine(EnemyBackToIdle(enemyAttackSpeed));
        }
    }

    private IEnumerator EnemyBackToIdle(float time)
    {
        yield return new WaitForSeconds(time);
        if (!isTargetInArea)
        {
            enemyMovement = EnemyMovement.IDLE;
        }
    }

    public void SetAnimator(bool isAim, int stateIdx)
    {
        if (animator == null) return;

        animator.SetBool("isAim", isAim);
        animator.SetInteger("State", stateIdx);
    }
}
