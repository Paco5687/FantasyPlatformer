using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{

    [SerializeField] public float attackBoxReach = 1f;
    public SpriteRenderer weaponRenderer;
    public Sprite newWeaponRenderer;

    //Singleton instantiation
    private static AttackBox instance;
    public static AttackBox Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<AttackBox>();
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        //If I touch an enemy, hurt the enemy
        if (col.gameObject.GetComponent<Enemy>())
        {
            col.gameObject.GetComponent<Enemy>().enemyHealth -= NewPlayer.Instance.playerAttackPower;
        }
    }

}

