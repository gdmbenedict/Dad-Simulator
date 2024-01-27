using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MowerController : MonoBehaviour
{
    // Movement serialized backend
    [SerializeField] float moveSpeed; // Move speed in m s -1
    [SerializeField] float rotSpeed;

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
            Vector3 moveVector = transform.forward * moveSpeed * Time.deltaTime;
            Vector3 movePos = moveVector + transform.position;
            rb.MovePosition(movePos);
        }
    }

    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }
}
