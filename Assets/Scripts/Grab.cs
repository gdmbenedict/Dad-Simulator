using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [Header("Grab Settings")]
    [SerializeField] private Transform holdArea;
    [SerializeField] private float grabRadius = 0.2f;
    [SerializeField] private string[] tags;
    private GameObject heldObject;
    private Rigidbody heldObjRB;

    [Header("Grabbed Object Physics")]
    [SerializeField] private float drag = 10f;
    [SerializeField] private float grabForce = 150.0f;
    [SerializeField] private float throwForce = 20.0f;
    [SerializeField] private float throwUpForce = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           // Debug.Log("Grab initiated");

            //check grab area for colliders
            Collider[] interactables = Physics.OverlapSphere(holdArea.position, grabRadius);
            foreach (Collider interactable in interactables)
            {
                //Debug.Log("Checking for interactables");
                //check if item is a tag that can be picked up
                if (tags.Contains(interactable.tag))
                {
                    //Debug.Log("grabbing object");
                    GrabObject(interactable.gameObject);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            DropObject();
        }

        if (heldObject != null)
        {
            MoveObject();
        }

        if (Input.GetMouseButtonDown(1) && heldObject != null)
        {
            throwObject();
        }
    }

    private void GrabObject(GameObject grabObj)
    {
        if (heldObject == null)
        {
            if (grabObj.GetComponent<Rigidbody>())
            {
                //rigidBody settings
                heldObjRB = grabObj.GetComponent<Rigidbody>();
                heldObjRB.useGravity = false;
                heldObjRB.drag = drag;
                heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

                heldObjRB.transform.parent = holdArea;

                //setting object
                heldObject = grabObj;
            }
        }
    }

    private void DropObject()
    {
        if (heldObject != null)
        {
            //rigidBody settings
            heldObjRB.useGravity = true;
            heldObjRB.drag = 1;
            heldObjRB.constraints = RigidbodyConstraints.None;

            heldObjRB.transform.parent = null;

            //setting object
            heldObject = null;
        }
    }

    private void MoveObject()
    {
        if (Vector3.Distance(heldObject.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObject.transform.position);
            heldObjRB.AddForce(moveDirection * grabForce);
        }
    }

    private void throwObject()
    {
        if (heldObject != null)
        {
            Debug.Log("throwing object");

            Vector3 ForceDirection = GetComponentInParent<Camera>().transform.forward * throwForce + transform.up * throwUpForce;

            heldObjRB.AddForce(ForceDirection, ForceMode.Impulse);
            DropObject();

        }
    }
}
