using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCleanUp : MonoBehaviour
{
    void Start(){       
    }
    void Update(){  
    }

    public GameObject carriedObject = null;
    public bool isCarryingSomething = false;
    public string laundryTag = "Laundry";
    public string garbageTag = "GarbageBag";
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the laundry
        if (!isCarryingSomething && other.CompareTag(laundryTag))
        {
            carry(other);
        }
        else if (!isCarryingSomething && other.CompareTag(garbageTag))
        {
            carry(other);
        }
    }

    // Method to carry object
    private void carry(Collider other)
    {
        isCarryingSomething = true;
        // Attach laundry object to the player
        other.transform.parent = transform;
        carriedObject = other.gameObject;

        // Optionally, you can disable physics to prevent it from continuing to fall or react to other physics events
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Adjust the position relative to the player
        Vector3 carryPosition = Vector3.zero; ;
        if (other.tag == laundryTag)
        {
            carryPosition = new Vector3(0, 0.5f, 0.5f);
        }else if(other.tag == garbageTag)
        {
            carryPosition = new Vector3(0.5f, 0.5f, 0.5f);
        }
        other.transform.localPosition = carryPosition;
    }
}
