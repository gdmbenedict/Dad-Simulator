using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveArm : MonoBehaviour
{
    [SerializeField] private float armSpeed=2;
    [SerializeField] private float maxDist=2;
    [SerializeField] private float minDist=0;
    private float armMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //getting input and applying multiplier
        armMovement = Input.GetAxis("Mouse ScrollWheel");
        float armChange = armMovement * armSpeed;

        //getting current arm position
        float armPos = gameObject.transform.localPosition.z;

        //checking bounds
        if (armPos + armChange >= maxDist)
        {
            armChange = maxDist - armPos;
        }
        else if (armPos + armChange <= minDist)
        {
            armChange = minDist - armPos;
        }

        //applying change in arm position
        gameObject.transform.localPosition += new Vector3(0f, 0f, armChange);

    }
}
