using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayerController : BulletBase
{
    private Vector3 movingDirection;

    private void FixedUpdate() {
        DestroyMoment();

        if (isMoving)
            this.transform.Translate(movingDirection * speed);
    }

    public void InitBullet(InventoryItems weaponAmmo, float additionalDamage) {

        this.gameObject.SetActive(true);

        switch (weaponAmmo) {
            case InventoryItems.AMMO_TYPE_1:
                damage = 2;
                movingDirection = new Vector3(1, 0);
                break;
            case InventoryItems.AMMO_TYPE_2:
                damage = 3;
                movingDirection = new Vector3(0, 1);
                break;
            case InventoryItems.AMMO_TYPE_3:
                damage = 4;
                movingDirection = new Vector3(0, 1);
                break;
            case InventoryItems.AMMO_TYPE_4:
                damage = 5;
                movingDirection = new Vector3(1, 0);
                break;
            case InventoryItems.NULL:
                damage = 1;
                movingDirection = new Vector3(0, 1);
                break;
        }
        damage += additionalDamage / 10;

        isMoving = true;
    }
}
