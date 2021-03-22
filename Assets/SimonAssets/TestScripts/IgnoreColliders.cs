using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreColliders : MonoBehaviour
{

  public Collider ignore_collider;

    void Awake()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), ignore_collider, true);

        if (ignore_collider == null)
          Debug.Log("No body capsual collider was found, check if the body is named Body");
    }



}
