using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCleanUpClothes : MonoBehaviour
{
    void Start(){       
    }
    void Update(){  
    }

    public GameObject carriedLaundry = null;
    public string laundryTag = "Laundry";
    private void OnTriggerEnter(Collider other)
    {
       
        // Check if the collider is the laundry
        if (other.CompareTag(laundryTag))
        {

            // Attach laundry object to the player
            other.transform.parent = transform;
            carriedLaundry = other.gameObject;

            // Optionally, you can disable physics to prevent it from continuing to fall or react to other physics events
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            // Adjust the position relative to the player as needed
            other.transform.localPosition = new Vector3(0, 0.5f, 0.5f);
        }
    }
}
