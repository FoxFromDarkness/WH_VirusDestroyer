using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Behaviour : MonoBehaviour
{ 
    public float speedOfRotation;
    public float speedOfMoving;
    private PlayerController player;

    //Moving
    private int movingDirection = 1;

    //PlayerFollowing
    private Vector2 direction;
    private float angle;
    private Quaternion rotation;

    private void Start() {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Moving();
        PlayerFollowing();
    }

    private void PlayerFollowing() {
        direction = -player.transform.position + this.transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speedOfRotation * Time.deltaTime);
    }

    private void Moving() {
        Vector3 direction = new Vector3(0, movingDirection, 0);
        transform.Translate(direction * speedOfMoving * Time.deltaTime);
    }
}
