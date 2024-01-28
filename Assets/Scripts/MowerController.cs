using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MowerController : MonoBehaviour
{
    // Movement serialized backend
    [SerializeField] float moveSpeed = 1; // Move speed in m s -1
    [SerializeField] float rotSpeed = 30; // Rot speed in deg s -1
    [SerializeField] Vector3 dismountOffset = new Vector3(2, 0, 0);
    [SerializeField] string playerObjName = "Player"; // Thats just what was on my clipboard.

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
        FindPlayer();
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
            if (moveValue.y != 0) // should we move?
            {
                // Multiply forward by speed and time to get desired distance for this update.
                Vector3 moveVector = transform.forward * moveSpeed * Time.deltaTime;
                if (moveValue.y < 0) { moveVector *= -0.5f; } // If we want to reverse, allow that, but make it slow.
                Vector3 movePos = moveVector + transform.position;
                rb.MovePosition(movePos);
            }
        }
    }

    void OnMove(InputValue value) // Get value of move controls to be used for turning/reversing
    {
        moveValue = value.Get<Vector2>();
    }

    void OnDismount(InputValue value) // Unset parent.
    {
        move = Convert.ToBoolean(value.Get<float>());

        // Unparent the player and move them.
        player.transform.parent = null;
        player.transform.position += dismountOffset;

        // Set cameras.
        m_Camera.enabled = false;
        playerCam.enabled = true;
    }

    void Mount() // Responsible for causing player to mount the mower.
    {
        // Snap player before parent.
        SnapPlayer();

        // set player parent to mower.
        player.transform.parent = gameObject.transform;
        playerCam.enabled = false;
        m_Camera.enabled = true;
    }

    void FindPlayer() // Use to find player, idk how, so do later.
    {
        // Return so we don't error.
        player = GameObject.Find(playerObjName);
        return;
    }

    void SnapPlayer() // Snap the player to their seat.
    {
        player.transform.position = playerAnchor.transform.position;
        player.transform.rotation = playerAnchor.transform.rotation;
    }

    private void OnTriggerEnter(Collider other) // check if player, if true, mount.
    {
        if(other.gameObject.name == player.name)
        {
            Mount();
        }
        //Debug.Log(playerObjName); Debug.Log(player.name);
    }
}
