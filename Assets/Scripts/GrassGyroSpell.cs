using System.Collections.Generic;
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
    List<bool> didFling = new List<bool>();

    // Update is called once per frame
    void FixedUpdate()
    {
        // Loop through grass objects to check things and fling them.
        for (int i = 0; i < grassObjs.Count; i++)
        {
            GameObject obj = grassObjs[i];
            if (obj != null)
            {
                Vector3 pos = obj.transform.position;
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                float timer = grassTimes[i];

                timer += Time.deltaTime;

                // If still travel time, drag grass towards center.
                if (timer < holdTime)
                {
                    // Figure out how far we're going.
                    float remainingTime = timer - travelTime;

                    // Move.
                    rb.MovePosition(Vector3.Lerp(pos, transform.position, remainingTime + 0.1f));
                    //Debug.Log(rb.velocity);
                }

                // if we didn't fling the grass, and we're not supposed to hold it anymore, fling the grass.
                if ((timer > holdTime) && timer < keepAliveTime && !didFling[i])
                {
                    rb.isKinematic = false;
                    rb.AddExplosionForce(force, pos + transform.right, explosionRadius);
                }

                if (timer > keepAliveTime) // if grass expired, kill it.
                {
                    grassObjs.Remove(grassObjs[i]);
                    grassTimes.Remove(grassTimes[i]);
                    didFling.Remove(didFling[i]);
                    Destroy(obj);
                }

                // At the end, put the timer back.
                grassTimes[i] = timer;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if(!grassObjs.Contains(other.gameObject)){
            grassObjs.Add(other.gameObject);
            grassTimes.Add(0);
            didFling.Add(false);
        }
    }
}
