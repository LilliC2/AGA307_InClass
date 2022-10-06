using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPad : MonoBehaviour
{
    public GameObject sphere;
    float count;
    bool exit;

    private void OnTriggerEnter(Collider other)
    {
        //change colour of sphere
        sphere.GetComponent<Renderer>().material.color = Color.green;
    }

    private void OnTriggerStay(Collider other)
    {
        //increase size of sphere by 0.01f
        sphere.transform.localScale += Vector3.one * 0.01f;
        count++;
        exit = false;
        
    }

    private void OnTriggerExit(Collider other)
    {
        //sphere.transform.localScale = Vector3.one;
        //reset colour and size of sphere
        sphere.GetComponent<Renderer>().material.color = Color.red;

        exit = true;


    }

    private void FixedUpdate()
    {
        if (exit == true && count > 0)
        {
            sphere.transform.localScale -= (Vector3.one * 0.01f);
            count--;
        }
        else if(count == 0) sphere.GetComponent<Renderer>().material.color = Color.white;
    }
    
}
