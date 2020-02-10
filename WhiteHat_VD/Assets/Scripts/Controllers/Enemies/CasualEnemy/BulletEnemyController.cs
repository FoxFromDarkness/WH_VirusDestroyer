using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyController : BulletBase
{
    public bool isLaser;

    private void FixedUpdate()
    {
        if (isLaser) return;

        DestroyMoment();

        if (isMoving)
            this.transform.Translate(new Vector3(1, 0) * speed);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<BulletPassObject>() && !collision.GetComponent<EnemyTriggerBehaviour>() && !isLaser)
            Destroy(this.gameObject);
    }
}
