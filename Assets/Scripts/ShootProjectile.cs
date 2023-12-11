using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject laser;
    public Transform shootPoint;
    public float laserSpeed = 10f;


    [SerializeField] private KeyCode shootButton;

    // Update is called once per frame
    void Update()
    {
        if (NewPlayer.Instance.playerWeaponEquipped == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
            // the direction of the instantiation is linked to the object's transfom.rotation
            Instantiate(laser, shootPoint.position, shootPoint.rotation);
    }
}
