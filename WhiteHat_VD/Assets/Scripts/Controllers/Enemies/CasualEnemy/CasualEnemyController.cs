using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasualEnemyController : MonoBehaviour
{
    public enum Type
    {
        PATROL,
        GUARD,
        KAMIKADZE,
        NONE
    }

    public enum Movement
    {
        IDLE,
        ATTACK,
        DIE,
        NONE
    }

    [SerializeField] private Transform body;
    private SpriteRenderer bodyImage;
    [SerializeField] private Type enemyType;
    [SerializeField] private Movement enemyMovement = Movement.IDLE;
    [SerializeField] float enemySpeed = 2;
    [SerializeField] bool movingRight = true;
    [SerializeField] bool isActive = true;

    private void Start()
    {
        bodyImage = body.GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

        switch (enemyMovement)
        {
            case Movement.IDLE:
                Idle();
                break;
            case Movement.ATTACK:
                break;
            case Movement.DIE:
                break;
            case Movement.NONE:
                break;
            default:
                break;
        }
    }

    private void Idle()
    {
        switch (enemyType)
        {
            case Type.PATROL:
                body.Translate(enemySpeed * Time.deltaTime * (movingRight == true ? 1 : -1), 0, 0);
                break;
            case Type.GUARD:
                break;
            case Type.KAMIKADZE:
                break;
            case Type.NONE:
                break;
            default:
                break;
        }
    }

    public void BorderCollision()
    {
        switch (enemyType)
        {
            case Type.PATROL:
                movingRight = !movingRight;
                bodyImage.flipX = !bodyImage.flipX;
                break;
            case Type.GUARD:
                break;
            case Type.KAMIKADZE:
                break;
            case Type.NONE:
                break;
            default:
                break;
        }
    }
}
