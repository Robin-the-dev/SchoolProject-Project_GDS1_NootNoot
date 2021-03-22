using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollScript : MonoBehaviour
{
  private Rigidbody ball_rb;
  public float max_vel;
  public float speed;
  public float thrust;
  public GroundDetection ground_detect;
  public TestPoleVault pole_vault;
    // Start is called before the first frame update
    void Start()
    {

        ball_rb = GetComponent<Rigidbody>();
        ball_rb.maxAngularVelocity = max_vel;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ball_rb.AddForce(transform.forward * thrust);
        //ball_rb.velocity *= speed;
    }
}
