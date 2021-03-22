using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterMass : MonoBehaviour
{

  //This script is used to visulise the center of mass on a gameobjects rigid body
  //And change where the center of mass is on that gameobject.
  private Rigidbody pole_rb;
  public Vector3 center_of_mass;

    void Start()
    {
        pole_rb = GetComponent<Rigidbody>();
        //pole_rb.position = new Vector3(0.0f,1.0f,-2.0f);
    }


    void Update()
    {
        pole_rb.centerOfMass = center_of_mass;
        pole_rb.WakeUp();
    }
    void OnDrawGizmos(){
      Gizmos.DrawSphere(transform.position + transform.rotation * center_of_mass,0.09f);
    }

}
