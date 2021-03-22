using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    private Rigidbody myRb;

    private BoxCollider collider;
    
    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        //Makes the banana fall on its own
        myRb.isKinematic = false;
        collider.enabled = true;
        transform.parent = null;
        
        //Call gameManager/ puzzle or whatnot
    }
}
