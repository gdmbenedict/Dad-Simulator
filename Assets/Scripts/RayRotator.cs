using UnityEngine;
using System.Collections;

public class RayRotator : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1000) * Time.deltaTime);
    }
}
