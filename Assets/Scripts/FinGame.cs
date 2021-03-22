using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = new Vector3(-130.14f,99.0f,49.65f);
            //GameManager.Instance.LevelComplete();
        }
    }
}
