using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory  
{


    private List<Collectable> itemList;

    public Inventory()
    {
        itemList = new List<Collectable>();
        AddInventoryItem(new Collectable { itemType = Collectable.ItemType.RangedWeapon, unique = 1 });
        Debug.Log(itemList.Count);
    }

    public void AddInventoryItem(Collectable collectable)
    {
        itemList.Add(collectable);
    }

    

}
