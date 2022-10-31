using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FiringPoint : MonoBehaviour
{
    public  GameObject projectilePrefab;
    public GameObject hitSparks;
    public float projectileSpeed = 1000f;
    public LineRenderer laser;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireRigidProjectile();
            
        }

        if(Input.GetButtonDown("Fire2"))
        {
            FireRayCast();
        }

    }

    void FireRigidProjectile()
    {
        //create a reference to hold our instantatied object
        //GameObject projectileInstance;

        //instantiate our projectile pregab at this object's position and
        //rotation
        GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, transform.rotation);

        //get the rigidbody attached to the projectile and add force to it
        projectileInstance.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
    }

    void FireRayCast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Debug.Log(hit.collider.name + " which was " + hit.distance + "units away");
            laser.SetPosition(0,transform.position);
            laser.SetPosition(1, hit.point);
            StopAllCoroutines();
            StartCoroutine(StopLaser());
            GameObject party = Instantiate(hitSparks, hit.point, hit.transform.rotation);
            Destroy(party, 2);


        }
    }

    IEnumerator StopLaser()
    {
        laser.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        laser.gameObject.SetActive(false);
    }
}

