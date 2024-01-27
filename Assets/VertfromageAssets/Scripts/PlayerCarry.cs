using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarry : MonoBehaviour
{

    public GameObject carriedObject = null;
    public Vector3 carryPosition = Vector3.zero; // Where to carry the object
    public float throwDistance = 2.0f;


    // Shaking variables
    public float shakeThreshold = 1.0f; // The threshold of shake intensity to detach the child
    public float shakeDuration = 2.0f; // Duration within which the player needs to shake
    private float shakeStartTime;
    private bool isShaking = false;

    public bool isCarryingSomething = false;

    // Tag variables
    public string laundryTag = "Laundry";
    public string garbageTag = "GarbageBag";
    public string childTag = "Child";


    private float originalYPositionObj;

    void Start(){       

    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.X))
        {
            throwObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCarryingSomething && other.CompareTag(childTag))
        {
            other.GetComponent<ChildScript>().AttachToPlayer();
            carry(other);
            
        }
        else if (!isCarryingSomething && other.CompareTag(laundryTag))
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
        originalYPositionObj = other.transform.position.y;
        isCarryingSomething = true;
        // Attach object to the player
        other.transform.parent = transform;
        carriedObject = other.gameObject;

        // Optionally, you can disable physics to prevent it from continuing to fall or react to other physics events
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        
        //        Example for using handPosition -- can do all this way if necessary    
        //        if(other.tag== beerTag)
        //       {
        //           carryPosition = handPosition;
        //       }

        // Adjust the position relative to the player
        if (other.tag == laundryTag)
        {
            carryPosition = new Vector3(0, 0.5f, 0.5f);
        }else if(other.tag == garbageTag)
        {
            carryPosition = new Vector3(0.5f, 0.5f, 0.5f);
        }
        other.transform.localPosition = carryPosition;
    }


    private void shakeItOff()
    {
        // only works for child
        if(carriedObject && carriedObject.tag != "Child")
        {
            return;
        }
        else
        {
            carriedObject.GetComponent<ChildScript>().DetachFromPlayer();
            detatchObject();
        }
    }

    public void throwObject()
    {
        // doesn't work for child
        if (carriedObject.tag == "Child")
        {
            return;
        }

        detatchObject();

    }

    private void detatchObject()
    {
        // undo attach to parent
        carriedObject.transform.parent = null;

        // move it away from player
        Vector3 localThrowDistance = transform.TransformDirection(new Vector3(0, 0, throwDistance));
        localThrowDistance.y = 0;
        // 
        Vector3 newPosition = carriedObject.transform.localPosition += localThrowDistance;
        newPosition.y = originalYPositionObj;

        carriedObject.transform.localPosition = newPosition;


        // undo disable physics
        Rigidbody rb = carriedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }


        // empty variables
        carriedObject = null;
        isCarryingSomething = false;
    }
}
