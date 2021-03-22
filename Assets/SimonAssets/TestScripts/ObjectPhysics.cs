using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPhysics : MonoBehaviour
{
    private float push_power;
    public Rigidbody hit_rb;
    private Rigidbody rb;

    void Start()
    {
       rb = GetComponentInParent<Rigidbody>();
    }
    //push objects not tagged player  in opposite direction hit by the push power
    void OnCollisionEnter(Collision hit)
    {

      if (hit_rb == null || hit_rb.isKinematic)
        return;
      if (transform.TransformDirection(hit_rb.velocity).y < -0.3)
        return;

      Vector3 push_dir = new Vector3(rb.velocity.x, 0.0f,
      rb.velocity.z );

      hit_rb.velocity = push_dir * push_power;

      // Debug.Log("dir: " + push_dir);


   }

}
