using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public bool Moving { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<RoboZombieController>() && !collision.GetComponent<RoboZombieController>().isDead)
        {
            collision.GetComponent<RoboZombieController>().isDead = true;
            collision.GetComponent<Animator>().SetBool("death", true);
        }

        Destroy(this.gameObject);
    }

    private void Update()
    {
        if(Moving)
            this.transform.Translate(new Vector3(1, 0));
    }
}
