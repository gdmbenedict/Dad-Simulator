using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarry : MonoBehaviour
{

    public GameObject carriedObject = null;
    public Vector3 carryPosition = Vector3.zero; // Where to carry the object
    public float throwDistance = 2.0f;


    // Shaking variables
    public int shakeCountRequired = 10; // Number of back-and-forth shakes required
    public float shakeThreshold = 0.5f; // Minimum movement threshold for a shake
    private int shakeCount = 0;
    private bool isShaking = false;
    private float lastMouseX;
    private bool lastDirection; // false = left, true = right

    public bool isCarryingSomething = false;

    // Tag variables
    public string laundryTag = "Laundry";
    public string garbageTag = "GarbageBag";
    public string childTag = "Child";

    // Carry Positions
    public Vector3 laundryCarryPos = new Vector3(0, 0.5f, 0.5f);
    public Vector3 garbageCarryPos = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 childCarryPos = new Vector3(0, 0.5f, 0.5f);

    private float originalYPositionObj;

    void Start(){       

    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.X))
        {
            throwObject();
        }
       
        // Logic to detatch child with mouse movements
        if (isCarryingSomething && carriedObject.tag==childTag)
        {
            
            if (!isShaking)
            {
                Debug.Log("shaking on!");
                StartShaking();
            }

            ProcessShake();

            if (shakeCount >= shakeCountRequired)
            {
                // Successfully shaken off
                isShaking = false;
                shakeCount = 0;
                shakeItOff(); // Implement this function based on your game's logic
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If running away child won't reattach
        if (other.CompareTag(childTag) && !other.GetComponent<ChildScript>().isRunningAway)
        {
            // If the child attacks you drop what you were carrying
            if (isCarryingSomething)
            {
                throwObject();
            }

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

        if(other.tag == childTag)
        {
            carryPosition = childCarryPos;
        }
        // Adjust the position relative to the player
        if (other.tag == laundryTag)
        {
            carryPosition = laundryCarryPos;
        } 
        if(other.tag == garbageTag)
        {
            carryPosition = garbageCarryPos;
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

    void StartShaking()
    {
        isShaking = true;
        lastMouseX = Input.mousePosition.x;
        lastDirection = Input.mousePosition.x >= Screen.width / 2; // Initial direction based on screen center
    }

    void ProcessShake()
    {
        float currentMouseX = Input.mousePosition.x;
        bool currentDirection = currentMouseX >= lastMouseX;

        if (currentDirection != lastDirection && Mathf.Abs(currentMouseX - lastMouseX) > shakeThreshold)
        {
            Debug.Log("Shake " + shakeCount);
            shakeCount++;
            lastDirection = currentDirection;
        }

        lastMouseX = currentMouseX;
    }
}
