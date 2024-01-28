using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steak : MonoBehaviour
{
    [SerializeField] private float cookTime =5f;
    private float cookTimer;
    private bool cooked;
    private bool burnt;

    [SerializeField] private Material cookedMaterial;
    [SerializeField] private Material burntMaterial;

    // Start is called before the first frame update
    void Start()
    {
        cookTimer = 0f;
        cooked = false;
        burnt = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cook()
    {
        cookTimer += Time.deltaTime;

        if (cookTimer >= cookTime && !cooked)
        {
            gameObject.GetComponent<MeshRenderer>().material = cookedMaterial;
            cooked = true;
        }

        if (cookTimer >= 2f*cookTime && !burnt)
        {
            gameObject.GetComponent<MeshRenderer>().material = burntMaterial;
            burnt = true;
        } 
    }
}
