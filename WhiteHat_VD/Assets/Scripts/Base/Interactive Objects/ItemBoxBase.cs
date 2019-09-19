using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxBase : MonoBehaviour
{
    public int ItemAmount;
    public Sprite sprite;
    public InventoryItems ItemType;

    private void Start()
    {
        if(sprite != null)
            this.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
