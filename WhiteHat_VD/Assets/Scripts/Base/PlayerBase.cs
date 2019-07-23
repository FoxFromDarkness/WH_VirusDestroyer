using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [Header("Statistics")]
    [SerializeField] private int hp;
    [SerializeField] private int hpMax;
    [SerializeField] private int level;
    [SerializeField] private int exp;
    [SerializeField] private int expToNextLvl;

    [Header("Inventory")]
    [SerializeField] private int blackCristals;
    public int BlackCristals
    {
        get { return blackCristals; }
        set { blackCristals = value; }
    }

    [SerializeField] private List<WeaponBase> weaponList;

    public Vector3 startPosition { get; set; }

    private void Start()
    {
        hpMax = 100;
        hp = hpMax;
        level = 0;
        expToNextLvl = 500;
        exp = 0;

        blackCristals = 0;
        startPosition = new Vector3(0, 0);
    }
}
