using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vault : MonoBehaviour
{
  private HingeJoint hinge;
  private TestPoleVault body_TestPoleVault;
  private Collider body_collider;
  public GameObject pole;
  private Vector3 anchor;
  private Vector3 connected_anchor;
  private GameObject new_pole;
  public bool hasJoint = false;
  //public float x,y,z;

  [Range(0, 1000)] public float force;
  [SerializeField] private float velocity;

  private Rigidbody headRb;

    // Start is called before the first frame update
    void Start()
    {
      //anchor = new Vector3(0.06031942f,0.06031942,0.01818657);
      //where we want the pole to be.
      anchor = new Vector3(0.0f,1.9f,0.0f);
      body_collider = GetComponent<Collider>();
      body_TestPoleVault = GetComponent<TestPoleVault>();
      connected_anchor = new Vector3(1.727f,0.00126921f,0.00126921f);
      headRb = GetComponent<Rigidbody>();
    }

    private void Update(){
      headRb.useGravity = false;
    }

    //Create hinge joint and a new pole that will attach to the head via the new joint.
    //This joint has a motor that spins the pole, lifting the player.
    public void PoleSpawn(){
      //spawn a new pole prefab.
      GameObject new_pole = pole;
      //pole.GetComponent<Rigidbody>().position = new Vector3(0.0f,1.0f,-2.0f);
      new_pole.GetComponent<IgnoreColliders>().ignore_collider = body_collider;
      if (!hasJoint){
        headRb.Sleep();
        hinge = gameObject.AddComponent<HingeJoint>();
        hinge.anchor = anchor;
        hinge.autoConfigureConnectedAnchor = true;
        //hinge.connectedAnchor = connected_anchor;
        hinge.connectedBody = Instantiate(new_pole,transform.localPosition + new Vector3(0.0f,1.9f,0.0f), transform.rotation
        ).GetComponent<Rigidbody>();
        hinge.autoConfigureConnectedAnchor = false;
        hinge.connectedAnchor = hinge.connectedAnchor - new Vector3(0.0f,0.0f,0.001f);
         JointMotor motor = hinge.motor;
         motor.force = force;
         motor.targetVelocity = -velocity;
         hinge.motor = motor;
         hinge.useMotor = true;
         hinge.massScale = 2.0f;
        headRb.WakeUp();
        hasJoint = true;
      }
    }
}
