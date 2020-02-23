using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public float HP { get; set; }
    public float HP_Max { get; set; }
    public float Level { get; set; }
    public static int Luck { get; set; }
    public float MagazineCapacity { get; set; }
    public float AdditionalDamage { get; set; }

    public int BlackCristals { get; set; }
    public WeaponBase[] WeaponMods { get; set; }
    public int[] AmmoBoxSupply { get; set; } 

    public Vector3 StartPosition { get; set; }

    private void Awake()
    {
        SetStartPlayerOptions();
    }

    public void SetStartPlayerOptions()
    {

        HP_Max = 100;
        HP = HP_Max;
        Level = 0;
        Luck = 0;
        MagazineCapacity = 10;
        AdditionalDamage = 0;

        WeaponMods = new WeaponBase[4];
        AmmoBoxSupply = new int[4];

        BlackCristals = 0;
        InitPlayerWeaponMods();
        //StartPosition = new Vector3(0, 0);
    }

    public string GetWeaponInfo(int idx)
    {
            return WeaponMods[idx].ToString();
    }

    public string GetAmmoBoxSupplyInfo(int idx)
    {
            return AmmoBoxSupply[idx] + "";
    }

    private void InitPlayerWeaponMods()
    {
        WeaponMods[0] = new WeaponBase();
        WeaponMods[1] = new WeaponBase();
        WeaponMods[2] = new WeaponBase();
        WeaponMods[3] = new WeaponBase();
    }
}
