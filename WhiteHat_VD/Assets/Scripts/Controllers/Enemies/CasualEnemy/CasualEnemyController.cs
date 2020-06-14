using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasualEnemyController : EnemyStateMovementMachine
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
    public Transform Body => body;
    [SerializeField] private Sprite[] bodyImages;
    [HideInInspector] public SpriteRenderer bodyImage;
    [HideInInspector]  public Collider2D bodyCollider;
    public RuntimeAnimatorController[] animators;
    [HideInInspector] public Animator animator;
    private HP_Canvas hpCanvas;

    [Space]
    [Header("Type")]
    public EnemyBody enemyBody;
    public EnemyType enemyType;
    [Tooltip("Only for information")]
    public EnemyAttack enemyAttack;

    [Space]
    [Header("Settings")]
    [SerializeField] private float maxHealthPoints = 100;
    private float currentHealthPoints;
    public float enemyMovingSpeed = 2;
    public float enemyAttackSpeed = 2;
    [Tooltip("Only for 'Repeating Attack'")]
    public int amountBulletsInSingleSeries = 3; //Ilość pocisków w serii (Repeating Atack)
    [HideInInspector] public float currentEnemyAttackTime = 5;

    [Space]
    [Header("Permissions")]
    public bool movingRight = true;
    public bool canMove = true;
    public bool canAttack = true;
    public bool isActive = true;
    [SerializeField] private bool flipAfterGuard = true;

    [Space]
    [Header("Animator")]
    [SerializeField] private bool _isAim;
    public bool IsAim { get { return _isAim; } set { _isAim = value; SetAnimator(_isAim, _currentState); } }
    [SerializeField] private int _currentState;
    public int CurrentState { get { return _currentState; } set { _currentState = value; SetAnimator(_isAim, _currentState); } }

    [Space]
    [Header("Weapons")]
    public BulletEnemyController enemyRiflegunBullet;
    public BulletEnemyController enemyShotgunBullet;
    public BulletEnemyController enemyRocketBullet;
    public BulletEnemyController enemyLaser;
    private Vector2 enemyRifleBulletStartPos;
    private Vector2 enemyRifleBulletStartScale;
    private Vector2 enemyShotgunBulletStartPos;
    private Vector2 enemyRocketStartPos;
    private Vector2 enemyLaserStartPos;
    private Vector2 enemyHPStartPos;
    public GameObject explosionEffect;

    [Space]
    [Header("Sounds")]
    [HideInInspector] public AudioSource audioSource;
    [SerializeField] private AudioClip[] bulletsSFX;

    //
    private PlayerController target;
    private bool isTargetInArea;
    //PlayerFollowing
    private Vector2 direction;
    private float angle;
    private Quaternion rotation;

    private void Start()
    {
        target = FindObjectOfType<PlayerController>();

        currentHealthPoints = maxHealthPoints;

        bodyImage = body.GetComponent<SpriteRenderer>();
        animator = body.GetComponent<Animator>();

        hpCanvas = body.GetComponentInChildren<HP_Canvas>();
        hpCanvas.gameObject.SetActive(false);

        if (enemyRiflegunBullet != null) enemyRifleBulletStartPos = enemyRiflegunBullet.transform.localPosition;
        if (enemyRiflegunBullet != null) enemyRifleBulletStartScale = enemyRiflegunBullet.transform.localScale;
        if(enemyShotgunBullet != null) enemyShotgunBulletStartPos = enemyShotgunBullet.transform.localPosition;
        if(enemyRocketBullet != null) enemyRocketStartPos = enemyRocketBullet.transform.localPosition;
        enemyLaserStartPos = enemyLaser.transform.localPosition;
        enemyHPStartPos = hpCanvas.transform.localPosition;
        audioSource = GetComponent<AudioSource>();

        if (enemyType == EnemyType.TOWER)
        {
            audioSource.clip = bulletsSFX[0];
            return;
        }

        switch (enemyBody)
        {
            case EnemyBody.BLACK:
                bodyImage.sprite = bodyImages[0];
                animator.runtimeAnimatorController = animators[0];
                audioSource.clip = bulletsSFX[0];
                break;
            case EnemyBody.RED:
                bodyImage.sprite = bodyImages[1];
                animator.runtimeAnimatorController = animators[1];
                audioSource.clip = bulletsSFX[1];
                break;
            default:
                bodyImage.sprite = bodyImages[2];
                animator.runtimeAnimatorController = animators[2];
                audioSource.clip = bulletsSFX[1];
                break;
        }

        SetEnemyState(new EnemyStateIdle(this));

        IsAim = _isAim;
        CurrentState = _currentState;
    }

    void Update()
    {
        if (!isActive) return;
        if(!GameController.IsInputEnable) return;

        enemyStateMovement.StartStateMovement();
    }

    private void CorrectAmmoPosition()
    {
        if (enemyRiflegunBullet != null)
        {
            enemyRiflegunBullet.transform.localPosition = enemyRifleBulletStartPos;
            enemyRiflegunBullet.transform.localPosition = new Vector3(enemyRiflegunBullet.transform.localPosition.x * (movingRight == true ? 1 : -1), enemyRiflegunBullet.transform.localPosition.y, 0);
            enemyRiflegunBullet.transform.localScale = enemyRifleBulletStartScale;
            enemyRiflegunBullet.transform.localScale = new Vector2(enemyRiflegunBullet.transform.localScale.x, enemyRiflegunBullet.transform.localScale.y * (movingRight == true ? 1 : -1));
        }

        if (enemyShotgunBullet != null)
        {
            enemyShotgunBullet.transform.localPosition = enemyShotgunBulletStartPos;
            enemyShotgunBullet.transform.localPosition = new Vector3(enemyShotgunBullet.transform.localPosition.x * (movingRight == true ? 1 : -1), enemyShotgunBullet.transform.localPosition.y, 0);
        }

        if (enemyRocketBullet != null)
        {
            enemyRocketBullet.transform.localPosition = enemyRocketStartPos;
            enemyRocketBullet.transform.localPosition = new Vector3(enemyRocketBullet.transform.localPosition.x * (movingRight == true ? 1 : -1), enemyRocketBullet.transform.localPosition.y, 0);
        }

        if (enemyAttack == EnemyAttack.LASER)
        {
            enemyLaser.transform.localPosition = enemyLaserStartPos;
            enemyLaser.transform.localPosition = new Vector3(enemyLaser.transform.localPosition.x * (movingRight == true ? 1 : -1), enemyLaser.transform.localPosition.y, 0);
        }
    }

    public void PlayerFollowing()
    {
        direction = -target.transform.position + body.transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, enemyMovingSpeed * Time.deltaTime);
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
            SetEnemyState(new EnemyStateDie(this));
        }
        else
        {
            //damageEffect.GetComponent<ParticleSystem>().Play();
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
                CorrectAmmoPosition();
                break;
            case EnemyType.GUARD:
                canMove = false;
                CurrentState = 0;
                if(flipAfterGuard) bodyImage.flipX = !bodyImage.flipX;
                CorrectAmmoPosition();
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
        CorrectAmmoPosition();


        if (isTargetInArea)
        {
            StopCoroutine(EnemyBackToIdle(0));
            SetEnemyState(new EnemyStateAttack(this));
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
            SetEnemyState(new EnemyStateIdle(this));
    }

    public void SetAnimator(bool isAim, int stateIdx)
    {
        if (animator == null) return;

        animator.SetBool("isAim", isAim);
        animator.SetInteger("State", stateIdx);
    }
}
