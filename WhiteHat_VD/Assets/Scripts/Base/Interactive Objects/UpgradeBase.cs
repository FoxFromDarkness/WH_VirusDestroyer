using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBase : MonoBehaviour
{
    [SerializeField]
    private Text percentUpgrade;
    [SerializeField]
    public int UpgradeAmount;
    [SerializeField]
    public PlayerAttributes PlayerAttributes;

    // Start is called before the first frame update
    void Start()
    {
        percentUpgrade.text = UpgradeAmount.ToString() + "%";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
