using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyController : BulletBase
{
    private void FixedUpdate()
    {
        DestroyMoment();

        if (isMoving)
            this.transform.Translate(new Vector3(1, 0) * speed);
    }
}
