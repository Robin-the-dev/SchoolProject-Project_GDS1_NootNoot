using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultLimit : MonoBehaviour
{
  public TestPoleVault ground_detect;

    void OnTriggerEnter(Collider other){
      if (other.tag == "Pole")
        ground_detect.isGrounded = true;
    }

}
