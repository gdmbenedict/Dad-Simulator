using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LawnMower : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform seat;

    [SerializeField] private float speed;

    private bool isMounted; //bool tracking if player is on the ride lawn mower
    

    // Start is called before the first frame update
    void Start()
    {
        isMounted = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isMounted)
        {
            player.transform.SetParent(transform);
        }
        else { 
        }

        if (isMounted && Input.GetMouseButton(0))
        {
            //calculates the relative position between the player and the seat
            Vector3 relativePosition = player.transform.position - seat.transform.position;

            //move lawn mower
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            
            //roatate

            //move player

        }
    }

    
    public void SetMounted(bool isMounted)
    {
        this.isMounted = isMounted; 
    }
}
