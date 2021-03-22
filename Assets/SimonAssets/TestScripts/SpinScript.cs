using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour
{
  public float speed;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(0.0f, speed, 0.0f);
    }
}
