using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotDoor : MonoBehaviour
{
  public Animator door_anim;
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter(Collider other){
      if (other.tag == "Guard")
        door_anim.SetBool("isOpen", true);
    }

    void OnTriggerExit(Collider other){
      if (other.tag == "Guard")
        door_anim.SetBool("isOpen", false);

    }

}
