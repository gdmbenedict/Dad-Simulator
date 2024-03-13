using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountTrigger : MonoBehaviour
{
    [SerializeField] private LawnMower lawnMower;

    private bool isMounted = false;
    
    // Update is called once per frame
    void Update()
    {
        //updates the bool of the lawn mower to see if the player is mounted
        lawnMower.SetMounted(isMounted);
    }

    //sets isMounted to true if player gets in area
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMounted = true;
        }
    }

    //sets isMounted to false if player leaves area
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMounted= false;
        }
    }
}
