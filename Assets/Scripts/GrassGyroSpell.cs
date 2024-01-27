using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrassGyroSpell : MonoBehaviour
{
    // Serialized
    [SerializeField] float travelTime = 1;
    [SerializeField] float holdTime = 3;
    [SerializeField] float keepAliveTime = 8;
    [SerializeField] float force = 50;
    [SerializeField] float explosionRadius = 5;

    // Backend
    List<GameObject> grassObjs = new List<GameObject>();
    List<float> grassTimes = new List<float>(); // Apparently I can't just use a struct list for some reason.
    List<bool> didFling = new List<bool> ();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < grassObjs.Count; i++)
        {
            GameObject obj = grassObjs[i];
            Vector3 pos = obj.transform.position;
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            float timer = grassTimes[i];

            timer += Time.deltaTime;

            // If still travel time, drag grass towards center.
            if(timer < holdTime)
            {
                // Figure out how far we're going.
                float remainingTime = timer - travelTime;

                // Move.
                rb.MovePosition(Vector3.Lerp(pos, transform.position, remainingTime + 0.1f));
                Debug.Log(rb.velocity);
            }

            if((!(timer < holdTime)) && timer < keepAliveTime && !didFling[i])
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(force, transform.position, explosionRadius);
            }

            if (timer > keepAliveTime)
            {
                grassObjs.RemoveAt(i);
                grassTimes.RemoveAt(i);
                didFling.RemoveAt(i);
                Destroy(obj);
            }

            // At the end, put the timer back.
            grassTimes[i] = timer;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        grassObjs.Add(other.gameObject);
        grassTimes.Add(0);
        didFling.Add(false);
    }
}