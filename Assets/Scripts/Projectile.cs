using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : GameBehaviour
{
    public int myDamage = 20;
    void Start()
    {
        Destroy(this.gameObject, 5);
        if(GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().pitch = Random.Range(0.5f, 1.3f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if we hit the object tagged enemy
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(collision.gameObject.GetComponent<Enemy>() != null)
            {
                //Run the Hit function on our Enemy
                collision.gameObject.GetComponent<Enemy>().Hit(myDamage);
                //destroy this object
                Destroy(gameObject);
            }
        }

        if(collision.gameObject.CompareTag("Target"))
        {
            if(collision.gameObject.GetComponent<Target>() != null)
            {
                collision.gameObject.GetComponent<Target>().Hit();
            }
        }

    }
}
