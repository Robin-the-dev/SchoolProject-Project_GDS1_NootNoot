using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
  public float speed;

  void Start(){
    
  }
    // Start is called before the first frame update
    void OnCollisionStay(Collision other){
      if (other.gameObject.tag == "Player"){
        other.rigidbody.useGravity = false;
        other.gameObject.GetComponent<TestPoleVault>().isGrounded = false;
        other.rigidbody.AddForce(Vector3.up*speed, ForceMode.VelocityChange);
        //other.transform.Translate(new Vector3(0.0f,2.0f,0.0f) * Time.deltaTime*speed);
      }
    }
}
