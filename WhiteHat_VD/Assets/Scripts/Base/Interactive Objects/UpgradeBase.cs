using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBase : MonoBehaviour
{
    private int actualPercentUpgrade = 0;
    private int hpPercentUpgrade = 0;
    private int luckPercentUpgrade = 0;
    private int magazineCapacityPercentUpgrade = 0;
    private int additionalDamagePercentUpgrade = 0;

    private InventoryItems InventoryItems;

    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private Text percentUpgrade;
    [SerializeField]
    private PlayerAttributes PlayerAttributes;
    [SerializeField]
    private int itemPrice; // Upgrade price
    [SerializeField]
    private int percentImprovement; // Upgrade % per buy

    public void Start()
    {
        percentUpgrade.text = actualPercentUpgrade.ToString() + "%";
    }

    public void OnClick()
    {
        if (player.GetItemAmount(InventoryItems.BLACK_CRISTALS) > itemPrice)
        {
            player.AddItem(InventoryItems.BLACK_CRISTALS, -itemPrice);  // test
            switch (PlayerAttributes)
            {
                case PlayerAttributes.HP_MAX:
                    hpPercentUpgrade = hpPercentUpgrade + percentImprovement;
                    percentUpgrade.text = hpPercentUpgrade.ToString() + "%";
                    break;
                case PlayerAttributes.LUCK:
                    luckPercentUpgrade = luckPercentUpgrade + percentImprovement;
                    percentUpgrade.text = luckPercentUpgrade.ToString() + "%";
                    break;
                case PlayerAttributes.MAGAZINE_CAPACITY:
                    magazineCapacityPercentUpgrade = magazineCapacityPercentUpgrade + percentImprovement;
                    percentUpgrade.text = magazineCapacityPercentUpgrade.ToString() + "%";
                    break;
                case PlayerAttributes.ADDITIONAL_DAMAGE:
                    additionalDamagePercentUpgrade = additionalDamagePercentUpgrade + percentImprovement;
                    percentUpgrade.text = additionalDamagePercentUpgrade.ToString() + "%";
                    break;
            }
            /*
             
            player.AddItem(InventoryItems.BLACK_CRISTALS, -itemPrice);  // implementation example
            switch (PlayerAttributes)
            {
                case PlayerAttributes.HP_MAX:
                    player.AddValue(PlayerAttributes.HP_MAX, +PlayerAttributes.HP_MAX * percentImprovement);
                    hpPercentUpgrade = hpPercentUpgrade + percentImprovement;
                    percentUpgrade.text = hpPercentUpgrade.ToString() + "%";
                    break;
                case PlayerAttributes.LUCK:
                    player.AddValue(PlayerAttributes.LUCK, +PlayerAttributes.LUCK * percentImprovement);
                    luckPercentUpgrade = luckPercentUpgrade + percentImprovement;
                    percentUpgrade.text = luckPercentUpgrade.ToString() + "%";
                    break;
                case PlayerAttributes.MAGAZINE_CAPACITY:
                    player.AddValue(PlayerAttributes.MAGAZINE_CAPACITY, +PlayerAttributes.MAGAZINE_CAPACITY * percentImprovement);
                    magazineCapacityPercentUpgrade = magazineCapacityPercentUpgrade + percentImprovement;
                    percentUpgrade.text = magazineCapacityPercentUpgrade.ToString() + "%";
                    break;
                case PlayerAttributes.ADDITIONAL_DAMAGE:
                    player.AddValue(PlayerAttributes.ADDITIONAL_DAMAGE, +PlayerAttributes.ADDITIONAL_DAMAGE * percentImprovement);
                    additionalDamagePercentUpgrade = additionalDamagePercentUpgrade + percentImprovement;
                    percentUpgrade.text = additionalDamagePercentUpgrade.ToString() + "%";
                    break;
            }
            */
            
        }
    }
}
