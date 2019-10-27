using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public static SaveController Instance;

    public PlayerController player;
    /*
        Player:
            Hp, 



     */

    private Dictionary<string, string> Prefs = new Dictionary<string, string>();


    private void Start()
    {
        if (Instance == null)
            Instance = FindObjectOfType<SaveController>();
    }

    public void SavePrefs()
    {
        Debug.Log("SaveController");

        Prefs.Clear();

        Prefs.Add("HP", player.PlayerStats.HP +"");
        Prefs.Add("HP_MAX", player.PlayerStats.HP_Max +"");
        Prefs.Add("Level", player.PlayerStats.Level +"");
        Prefs.Add("Exp", player.PlayerStats.Exp +"");
        Prefs.Add("ExpToNextLvl", player.PlayerStats.ExpToNextLvl +"");

        Prefs.Add("BlackCristals", player.PlayerStats.BlackCristals + "");

        Prefs.Add("Weapon_0", player.PlayerStats.GetWeaponInfo(0));
        Prefs.Add("Weapon_1", player.PlayerStats.GetWeaponInfo(1));
        Prefs.Add("Weapon_2", player.PlayerStats.GetWeaponInfo(2));
        Prefs.Add("Weapon_3", player.PlayerStats.GetWeaponInfo(3));

        Prefs.Add("AmmoBoxSupply_0", player.PlayerStats.GetAmmoBoxSupplyInfo(0));
        Prefs.Add("AmmoBoxSupply_1", player.PlayerStats.GetAmmoBoxSupplyInfo(1));
        Prefs.Add("AmmoBoxSupply_2", player.PlayerStats.GetAmmoBoxSupplyInfo(2));
        Prefs.Add("AmmoBoxSupply_3", player.PlayerStats.GetAmmoBoxSupplyInfo(3));

        Dictionary<string, string>.ValueCollection valueColl = Prefs.Values;

        foreach (string s in valueColl)
        {
            Debug.Log(s);
        }

    }
}
