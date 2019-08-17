using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxBase : MonoBehaviour
{
    [SerializeField]
    private int ammoAmount;
    public int AmmoAmount { get { return ammoAmount; } }
    [SerializeField]
    private InventoryItems ammoType;
    public InventoryItems AmmoType { get { return ammoType; } }
}
