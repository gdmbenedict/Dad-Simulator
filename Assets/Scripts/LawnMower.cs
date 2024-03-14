using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LawnMower : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform seat;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed;

    private bool isMounted; //bool tracking if player is on the ride lawn mower
    private bool isMoving;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isMounted = false;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isMounted && Input.GetMouseButton(0))
        {
            //set moving to true
            isMoving = true;

            //roatate
            

            //move player

        }
        else
        {
            isMoving = false;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Vector3 relativeposition = player.transform.InverseTransformPoint(transform.position);

            Vector3 newPosition = (transform.position) + (transform.right * speed) * Time.deltaTime;
            Debug.Log("Moving being called");
            rb.MovePosition(newPosition);

            player.transform.position += relativeposition;
        }
    }


    public void SetMounted(bool isMounted)
    {
        this.isMounted = isMounted; 
    }
}
