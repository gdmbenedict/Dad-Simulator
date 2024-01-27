using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffScript : MonoBehaviour
{
    public float delay = 0.2f;
    public string acceptedItemTag = "";
    private void OnTriggerEnter(Collider other)
    {
        PlayerCarry playerScript = other.GetComponent<PlayerCarry>();
        if (playerScript != null && playerScript.carriedObject != null && playerScript.carriedObject.tag==acceptedItemTag)
        {
            Destroy(playerScript.carriedObject, delay);
            playerScript.carriedObject = null;
            playerScript.isCarryingSomething = false;
        }
    }
}
