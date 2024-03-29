using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildScript : MonoBehaviour
{
    // public vars
    public Transform player; // Assign the player's Transform in the inspector
    public float detectionRange = 10f; // The range within which the child detects the player
    public float moveSpeed = 5f; // Speed at which the child moves towards the player
    public float rotationSpeed = 5f; // Speed at which the child rotates towards the player
    public float resetDelay = 0.5f;

    // Private vars
    private bool isAttached = false; // Tracks whether the child is attached to the player
    public bool isRunningAway = false; // checks if the child has been shaken off
    private Vector3 originalPosition;
    public float stopThreshold = 0.1f; // Threshold distance to stop moving towards original position

    // NavMesh
    public UnityEngine.AI.NavMeshAgent agent;

    // Sound
    public AudioClip attachmentSound; // The sound clip to play when attached
    private AudioSource audioSource; // The AudioSource component
    private void Start()
    {
        originalPosition = transform.position; // set original pos

        // set up navMesh
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = originalPosition;

        // Get the AudioSource component attached to this object
        audioSource = GetComponent<AudioSource>();
        // If there's no AudioSource component, add one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        // If the child is attached, don't move or rotate
        if (isAttached) return;

        if (isRunningAway)
        {
            Debug.Log("Running away!");

            // Might need this...
            //    Vector3 directionToOrigin = (originalPosition - transform.position).normalized;

            //    // Rotate the child to face the start point
            //    Quaternion lookRotation = Quaternion.LookRotation(directionToOrigin);
            //    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
      
            return;
        }

        // Calculate the distance between the child and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the detection range
        if (distanceToPlayer <= detectionRange)
        {
            // might need this...
            //Vector3 directionToPlayer = (player.position - transform.position).normalized;

            //// Rotate the child to face the player
            //Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            agent.destination = player.transform.position;
        }
    }

    // Call this method to attach the child
    public void AttachToPlayer()
    {
        isAttached = true;
        PlayAttachmentSound();
    }

    public void DetachFromPlayer()
    {
        isAttached = false;
        isRunningAway = true;
        agent.destination = originalPosition;
        StopAttachmentSound();
        Invoke("resetChild", resetDelay);
    }

    private void PlayAttachmentSound()
    {
        if (attachmentSound != null)
        {
            audioSource.clip = attachmentSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void StopAttachmentSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.loop = false;
            audioSource.Stop();
        }
    }

    private void resetChild()
    {
        // The actions to perform after the delay
        Debug.Log("Function called after delay.");
        isRunningAway = false;
    }
}
