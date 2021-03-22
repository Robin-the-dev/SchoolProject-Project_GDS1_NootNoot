using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SkyWalk : MonoBehaviour
{
  public Animator elevator_anim;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void MachineGo()
    {
      elevator_anim.SetBool("isMove", true);
    }

    void OnCollisionExit(Collision other)
    {
      if(other.transform.gameObject.tag == "Player" && other.transform.parent != null){
        other.transform.parent = null;
      }
    }
}
