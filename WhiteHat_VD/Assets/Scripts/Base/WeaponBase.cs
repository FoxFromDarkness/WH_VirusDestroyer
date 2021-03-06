﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public bool IsUnlock { get; set; }
    public InventoryItems TypeOfAmmo { get; set; }
    public int AmmoAmount { get; set; }
   
    public WeaponBase()
    {
        IsUnlock = false;
        this.TypeOfAmmo = InventoryItems.NULL;
        AmmoAmount = 0;
    }

    public override string ToString()
    {
        return $"{IsUnlock}|{(int)TypeOfAmmo}|{AmmoAmount}";
        //return $"IsUnlock{IsUnlock}TypeOfAmmo{TypeOfAmmo}AmmoAmount{AmmoAmount}";
    }
}
