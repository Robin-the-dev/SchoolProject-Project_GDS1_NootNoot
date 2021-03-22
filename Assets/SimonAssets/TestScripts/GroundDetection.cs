using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{

  private TestPoleVault pole_vault;
  public bool rollmode;
  public GameObject ball;
  public float floor_distance;
  private Rigidbody rb;
  public Animator anim;

  void Start(){
    rb = GetComponent<Rigidbody>();
    rollmode = false;
    pole_vault = GetComponent<TestPoleVault>();
  }

  void Update(){

    RampCheck();
    GroundCheck();

    if (!rollmode)
      ball.SetActive(false);
    else
      ball.SetActive(true);



  }

  void OnCollisionEnter(Collision other)
  {
    if (other.transform.gameObject.tag == "Ground")
    {
      pole_vault.isGrounded = true;
      rollmode = false;
      rb.velocity = Vector3.zero;
      rb.angularVelocity = Vector3.zero;
      rb.mass = 1000;
      StartCoroutine(WaitTime());
    }
    else
      pole_vault.isGrounded = false;
  }

  /*void OnCollisionExit(Collision other){
    if (other.transform.gameObject.tag == "Ground")
      pole_vault.isGrounded = false;
  }*/

void GroundCheck(){
  if (!pole_vault.isGrounded && !pole_vault.isVaulting){
    RaycastHit hit;
    if (Physics.Raycast(transform.localPosition, Vector3.down, out hit, 0.1f))
      if(hit.collider.tag == "Ground")
        pole_vault.isGrounded = true;
  }
}

  void RampCheck(){
    RaycastHit hit;
    if(Physics.Raycast(transform.position, Vector3.down, out hit, floor_distance)){
      if (hit.transform.gameObject.tag == "Ramp"){
        rollmode = true;
        pole_vault.isGrounded = false;
        anim.SetBool("isRolling", true);
      }
      else{
        rollmode = false;
        anim.SetBool("isRolling", false);
      }
    }

  }

  IEnumerator WaitTime(){
    yield return new WaitForSeconds(0.1f);
    rb.mass = 1;
  }


}
