using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform tpToLocation;
    
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = tpToLocation.position;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
