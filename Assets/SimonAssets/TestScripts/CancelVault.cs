using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelVault : MonoBehaviour
{
  public TestPoleVault ground_detect;

    void OnTriggerEnter(Collider other){
      if (other.tag == "Player" || other.tag == "Pole")
        ground_detect.isGrounded = true;
        
    }
}
