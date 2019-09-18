using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayerController : BulletBase
{
    private ParticleSystem[] bulletEffects;

    private void Awake() {
        bulletEffects = GetComponentsInChildren<ParticleSystem>();
        foreach (var item in bulletEffects)
            item.Stop();
    }

    private void Start()
    {
        damage = 1;
    }

    protected override void OnTriggerEnter2D(Collider2D collision) {

        //if (collision.GetComponent<RoboZombieController>() && !collision.GetComponent<RoboZombieController>().IsDead) {
        //    collision.GetComponent<RoboZombieController>().IsDead = true;
        //    collision.GetComponent<Animator>().SetBool("death", true);
        //}

        base.OnTriggerEnter2D(collision);
    }

    private void FixedUpdate() {
        DestroyMoment();

        if (isMoving)
            this.transform.Translate(new Vector3(1, 0) * speed);
    }

    public void InitBullet(InventoryItems weaponAmmo) {
        this.gameObject.SetActive(true);
        isMoving = true;
        switch (weaponAmmo) {
            case InventoryItems.AMMO_TYPE_1:
                damage *= 1.1f;
                bulletEffects[0].Play();
                break;
            case InventoryItems.AMMO_TYPE_2:
                damage *= 1.2f;
                bulletEffects[1].Play();
                break;
            case InventoryItems.AMMO_TYPE_3:
                damage *= 1.3f;
                bulletEffects[2].Play();
                break;
            case InventoryItems.AMMO_TYPE_4:
                damage *= 1.4f;
                bulletEffects[3].Play();
                break;
            case InventoryItems.NULL:
                damage = 1;
                break;
        }
    }
}
