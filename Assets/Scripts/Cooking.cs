using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : MonoBehaviour
{
    [SerializeField] private float stoveRadius = 0.1f;
    [SerializeField] private float cookRadius = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //checks if by stove
        if (CheckStove())
        {
            Collider[] objectColliders = Physics.OverlapSphere(gameObject.transform.position, cookRadius);
            foreach (var collider in objectColliders)
            {
                if(collider.gameObject.tag == "Steak")
                {
                    collider.gameObject.GetComponent<Steak>().Cook();
                }
            }
        }
    }

    //function that checks for nearby stove
    private bool CheckStove()
    {
        bool stove = false;

        //check all nearby objects for stove
        Collider[] objectColliders = Physics.OverlapSphere(gameObject.transform.position, stoveRadius);
        foreach(var collider in objectColliders)
        {
            if (collider.gameObject.tag == "Stove")
            {
                stove = true;

                Debug.Log(stove);
            }
        }

        return stove;
    } 
}
