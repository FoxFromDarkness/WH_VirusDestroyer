using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyController : BulletBase
{
    public bool isLaser;
    [SerializeField] private bool isTowerBullet;

    private void FixedUpdate()
    {
        if (isLaser) return;

        DestroyMoment();

        if (isMoving)
        {
            if(isTowerBullet) this.transform.Translate(new Vector3(1, 0) * speed);
            else this.transform.Translate(new Vector3(0, 1) * speed);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<BulletPassObject>() && !collision.GetComponent<EnemyTriggerBehaviour>() && !isLaser)
            Destroy(this.gameObject);
    }
}
