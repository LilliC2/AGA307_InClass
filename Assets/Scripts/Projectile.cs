using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void Start()
    {
        

        Destroy(gameObject,5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Target"))
        {
            //Debug.Log(collision.gameObject.name);
            //check ig we hit the object tagged target

            //change colour of target
            //to be colour specific; new Color.(R,G.B)
            collision.gameObject.GetComponent<Renderer>().material.color = Color.red;

            //destroy target after 1 sec

            Destroy(collision.gameObject,1);

            //destroy this object
            Destroy(gameObject);
        }
    }


}
