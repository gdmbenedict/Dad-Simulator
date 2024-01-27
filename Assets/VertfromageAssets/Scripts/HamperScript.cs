using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamperScript : MonoBehaviour
{
    public float delay = 0.2f;
    private void OnTriggerEnter(Collider other)
    {

        PlayerCleanUpClothes playerScript = other.GetComponent<PlayerCleanUpClothes>();
        if (playerScript != null && playerScript.carriedLaundry != null)
        {
            Destroy(playerScript.carriedLaundry, delay);
            playerScript.carriedLaundry = null;
        }
    }
}
