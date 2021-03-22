using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHinge : MonoBehaviour
{
  private HingeJoint hinge;
  public TestPoleVault body_TestPoleVault;
  public Collider body_collider;
  public GameObject pole;
  private Vector3 anchor;
  private Vector3 connected_anchor;
  private GameObject new_pole;

  [SerializeField] private Transform player;
  [Range(0, 1000)] public float force;
  [SerializeField] private float velocity;

  private Rigidbody headRb;

    // Start is called before the first frame update
    void Start()
    {
      anchor = new Vector3(0.0f,0.0f,0.0f);
      //where we want the pole to be.
      connected_anchor = new Vector3(0.0f,0.0f,-0.000762047f);
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
      new_pole.GetComponent<IgnoreColliders>().ignore_collider = body_collider;

      headRb.Sleep();
      hinge = gameObject.AddComponent<HingeJoint>();
      hinge.anchor = anchor;
      hinge.connectedBody = Instantiate(new_pole,transform.position, player.rotation).GetComponent<Rigidbody>();
      JointMotor motor = hinge.motor;
      motor.force = force;
      motor.targetVelocity = -velocity;
      hinge.motor = motor;
      hinge.useMotor = true;
      hinge.autoConfigureConnectedAnchor = false;
      hinge.connectedAnchor = connected_anchor;
      hinge.massScale = 2.0f;
      headRb.WakeUp();
    }


}
