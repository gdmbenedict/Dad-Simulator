using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MowerController : MonoBehaviour
{
    // Movement serialized backend
    [SerializeField] float moveSpeed = 1; // Move speed in m s -1
    [SerializeField] float rotSpeed = 30; // Rot speed in deg s -1

    // Movement backend things
    Vector2 moveValue;
    Rigidbody rb;


    void Start()
    {
        // Get component(s)
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Movement things
        {
            // Rotation
            Vector3 rot = new Vector3(0, moveValue.x, 0) * rotSpeed * Time.deltaTime;
            transform.Rotate(rot);

            // Movement
            Vector3 moveVector = transform.forward * moveSpeed * Time.deltaTime;
            if(moveValue.y < 0) { moveVector *= -0.5f; } // If we want to reverse, allow that, but make it slow.
            Vector3 movePos = moveVector + transform.position;
            rb.MovePosition(movePos);
        }
    }

    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }
}
