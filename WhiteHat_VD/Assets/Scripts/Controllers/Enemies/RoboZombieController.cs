using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboZombieController : MonoBehaviour
{
    public bool IsDead { get; set; }

    private void FixedUpdate()
    {
        Vector2 playerPos = this.transform.position;
        Vector2 zommbiePos = this.transform.position;

        if(Vector3.Distance(playerPos, zommbiePos) < 1.0f)
        {
            this.GetComponent<Animator>().SetBool("attack", true);
        }
    }
}
