using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    public PlayerCarry playerScript = null;

    // Start is called before the first frame update
    void Start()
    {
        if (playerScript == null)
        {
            Debug.LogError("PlayerCarry script not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            Debug.Log("Hand Touched " + other.tag);
            playerScript.handleHandTriggerEnter(other, transform);
        }
        else
        {
            Debug.Log("Collider is null");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            Debug.Log("Hand leaving " + other.tag);
            playerScript.handleHandTriggerExit();
        }
        else
        {
            Debug.Log("Collider is null");
        }
    }
}
