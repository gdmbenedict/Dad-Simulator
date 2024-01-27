using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePush : MonoBehaviour
{
    [SerializeField] float forceMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function called when the player controller collides with a rigidbody
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //gets rigid body of thing that is being collided with
        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if(rigidbody != null)
        {
            //get force direction
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;

            //Y value clamping (not implemented yet)

            forceDirection.Normalize();

            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }
    }
}
