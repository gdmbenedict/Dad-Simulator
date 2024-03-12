using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GrabOther : MonoBehaviour
{
    private bool hold;
    public KeyCode grabKey;
    public bool canGrab;

    void Update()
    {
        if (canGrab)
        {
            if (Input.GetKey(grabKey))
            {
                hold = true;
            }
            else
            {
                hold = false;
                Destroy(GetComponent<FixedJoint>());
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.CompareTag("Item"))
        {
            if (hold && col.transform.tag != "Player")
            {
                Rigidbody rb = col.transform.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
                    fj.connectedBody = rb;
                }
                else
                {
                    FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
                }
            }
        }
        else if (col.gameObject.CompareTag("Steak"))
        {
            if (hold && col.transform.tag != "Player")
            {
                Rigidbody rb = col.transform.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
                    fj.connectedBody = rb;
                }
                else
                {
                    FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
                }
            }
        }
    }
}