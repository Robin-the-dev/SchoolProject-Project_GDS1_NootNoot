using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField]public static bool isWater = false;

    [SerializeField] private float waterDrag = 7f;
    private float originDrag;

    // Start is called before the first frame update
    void Start()
    {
        originDrag = 0;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Player") {
            getInWater(other);
        }
    }

    private void getInWater(Collider collider) {
        isWater = true;
        

        collider.transform.GetComponent<Rigidbody>().drag = waterDrag;
    }

    private void OnTriggerExit(Collider other) {
        if(other.transform.tag == "Player") {
            getOutWater(other);
        }
    }

    private void getOutWater(Collider collider) {
        if (isWater) {
            isWater = false;
            collider.transform.GetComponent<Rigidbody>().drag = originDrag;
        }
    }
}
