using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleGrate : MonoBehaviour
{
    [SerializeField] private GameObject destroyedVersion;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Beak"))
        {
            if (other.gameObject.GetComponentInParent<TestBeakTween>().isStretching)
            {
                Instantiate(destroyedVersion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        //Debug.Log(other.gameObject.tag);
    }
}
