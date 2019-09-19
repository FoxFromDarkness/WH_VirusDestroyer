using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyTowerController : BulletBase
{
    private void Start()
    {
        damage = 10;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //base.OnTriggerEnter2D(collision);
    }

    private void FixedUpdate()
    {
        DestroyMoment();

        if (isMoving)
            this.transform.Translate(new Vector3(0, -1) * speed);
    }
}
