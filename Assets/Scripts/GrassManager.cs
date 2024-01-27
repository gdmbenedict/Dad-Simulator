using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassManager : MonoBehaviour
{
    // Serialized
    [SerializeField] int grassCount = 25;

    // Backend
    List<GameObject> grassList = new List<GameObject>();
    System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        // Find and add first grass to list.
        GameObject Grass1 = GameObject.Find("GrassObject"); grassList.Add(Grass1);
        for (int i = 1; i < grassCount; i++)
        {
            GameObject newGrassObj = Instantiate(Grass1);
            newGrassObj.transform.position = new Vector3(random.Next(-9, 10), 0, random.Next(-9, 10));
            grassList.Add(newGrassObj);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
