using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    //Dropdown in the inspector for weapon type
    enum WeaponType { Fist, Melee, Ranged, AreaOfEffect }
    [Header("Weapon Stats")]
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private int ammoPerClip;
    [SerializeField] private int reloadTime;
    [SerializeField] private int weaponDurability;

    [Header("References")]
    [SerializeField] public string weaponStringName;
    [SerializeField] private Sprite weaponSprite;
    [SerializeField] public int weaponId;
    

    //Singleton instantiation   "Weapons.Instance"
    private static Weapons instance;
    public static Weapons Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<Weapons>();
            return instance;
        }
    }

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
            if (weaponType == WeaponType.Fist)
            {
                
            }
            else if (weaponType == WeaponType.Ranged)
            {
                //Shows the weapon sprite in the top right corner. Will not keep here long term
                NewPlayer.Instance.AddWeapon(weaponStringName, weaponSprite);

            }
            else if (weaponType == WeaponType.Melee)
            {

            }
            else if (weaponType == WeaponType.AreaOfEffect)
            {
                
            }
            NewPlayer.Instance.UpdateUI();
            Destroy(gameObject);
        }
    }
}
