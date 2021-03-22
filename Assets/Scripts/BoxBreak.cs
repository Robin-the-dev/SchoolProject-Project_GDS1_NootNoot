using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreak : MonoBehaviour
{
    [SerializeField] private GameObject fish;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Beak"))
        {    
            fish.GetComponent<Collider>().enabled = true;
            fish.GetComponent<Rigidbody>().isKinematic = false;
            fish.transform.parent = null;
            
            Destroy(gameObject);
        }
    }
}
