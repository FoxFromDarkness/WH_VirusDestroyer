using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public int HP { get; set; }
    public int HP_Max { get; set; }
    public int Level { get; set; }
    public int Exp { get; set; }
    public int ExpToNextLvl { get; set; }

    public int BlackCristals { get; set; }
    public WeaponBase[] WeaponMods { get; set; }
    public int[] AmmoBoxSupply { get; set; } 

    public Vector3 StartPosition { get; set; }

    private void Awake()
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
