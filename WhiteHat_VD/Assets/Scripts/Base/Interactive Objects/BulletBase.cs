using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float damage = 1;
    public float speed = 1;
    public bool isMoving = false;
    
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.GetComponent<BulletPassObject>())
            Destroy(this.gameObject);
    }

    protected void DestroyMoment() {
        if (this.transform.localPosition.x > 300 || this.transform.localPosition.x < -300)
            Destroy(this.gameObject);
    }
}
