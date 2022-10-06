using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject DoorLeft;
    public GameObject DoorRight;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            DoorLeft.SetActive(false);
            DoorRight.SetActive(false);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            DoorLeft.SetActive(true);
            DoorRight.SetActive(true);
    }
}
