using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    //Dropdown in the inspector for item type
    public enum ItemType { Coin, Health, InventoryItem, RangedWeapon, MeleeWeapon }
    [Header("References")]
    [SerializeField] private int coinValue = 1;
    [SerializeField] public ItemType itemType;
    [SerializeField] private string inventoryStringName;
    [SerializeField] private Sprite inventorySprite;
    [SerializeField] private int healthModifier = 10;
    [SerializeField] public int unique;

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

    }

    // When you collect a coin it adds one coin to your inventory
    // When you collect health it increases your health up to your max
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == NewPlayer.Instance.gameObject)
        {
            if (itemType == ItemType.Coin)
            {
                NewPlayer.Instance.coinsCollected += coinValue;
            }
            else if (itemType == ItemType.Health)
            {
                if (NewPlayer.Instance.playerHealth < NewPlayer.Instance.playerMaxHealth)
                {
                    NewPlayer.Instance.playerHealth += healthModifier;
                }

            }
            else if (itemType == ItemType.InventoryItem)
            {
                NewPlayer.Instance.AddInventoryItem(inventoryStringName, inventorySprite);
            }

            NewPlayer.Instance.UpdateUI();
            Destroy(gameObject);
        }
    }
}
