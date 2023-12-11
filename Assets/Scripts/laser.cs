using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    public float laserSpeed;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * laserSpeed;
    }



    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.GetComponent<Enemy>())
        {
            hitInfo.gameObject.GetComponent<Enemy>().enemyHealth -= NewPlayer.Instance.playerAttackPower;
            Destroy(gameObject);
        }

        Destroy(gameObject, 3);
    }
}
