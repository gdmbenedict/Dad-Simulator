using System;
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
    [SerializeField] bool move = true;

    // Player snapping backend
    GameObject player;
    GameObject playerAnchor;
    bool doSnapping = false;
    Camera playerCam;
    Camera m_Camera;

    void Start()
    {
        // Find GameObjects.
        //player = FindPlayer();
        playerAnchor = GameObject.Find("PlayerAnchor");

        // Get component(s)
        rb = GetComponent<Rigidbody>();
        m_Camera = GameObject.Find("JohnBeerCam").GetComponent<Camera>();
        playerCam = Camera.main;
    }

    void FixedUpdate()
    {
        // Movement things
        {
            // Rotation
            Vector3 rot = new Vector3(0, moveValue.x, 0) * rotSpeed * Time.deltaTime;
            transform.Rotate(rot);

            // Movement
            if (move) // should we move?
            {
                // Multiply forward by speed and time to get desired distance for this update.
                Vector3 moveVector = transform.forward * moveSpeed * Time.deltaTime;
                if (moveValue.y < 0) { moveVector *= -0.5f; } // If we want to reverse, allow that, but make it slow.
                Vector3 movePos = moveVector + transform.position;
                rb.MovePosition(movePos);
            }

            if (doSnapping)
            {
                // Snap player.
                SnapPlayer();
            }
            else // Use else to unset camera as maincamera.
            {
                m_Camera.enabled = false;
                playerCam.enabled = true;
            }
        }
    }

    void OnMove(InputValue value) // Get value of move controls to be used for turning/reversing
    {
        moveValue = value.Get<Vector2>();
    }

    void OnActivate(InputValue value) // Get value of activate to determine if we should move, and snap player.
    {
        move = Convert.ToBoolean(value.Get<float>());

        // Also set snapping to true, so player gets snapped to chair.
        doSnapping = true;

        // Set cameras.
        playerCam.enabled = false;
        m_Camera.enabled = true;
    }

    GameObject FindPlayer() // Use to find player, idk how, so do later.
    {
        // Return player so we don't error.
        return player;
    }

    void SnapPlayer() // Snap the player to their seat.
    {
        player.transform.position = playerAnchor.transform.position;
        player.transform.rotation = playerAnchor.transform.rotation;
    }
}
