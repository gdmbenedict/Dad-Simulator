using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    private PlayerCarry playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<PlayerCarry>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        playerScript.handleHandTrigger(other, transform);
    }
}
