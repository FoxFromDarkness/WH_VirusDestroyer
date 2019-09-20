using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public float HP { get; set; }
    public float HP_Max { get; set; }
    public float Level { get; set; }
    public float Exp { get; set; }
    public float ExpToNextLvl { get; set; }

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
        ExpToNextLvl = 500;
        Exp = 0;

        WeaponMods = new WeaponBase[4];
        AmmoBoxSupply = new int[4];

        BlackCristals = 0;
        StartPosition = new Vector3(0, 0);
    }
}
