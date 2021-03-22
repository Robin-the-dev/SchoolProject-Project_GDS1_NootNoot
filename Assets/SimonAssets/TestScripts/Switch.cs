using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Switch : MonoBehaviour
{
    public GameObject trigger;
    public TestBeakTween nooted;
    private bool cooldown = false;
    public GameObject button;
    private Material initial_material;
    public Material red_mat;

    void Start(){
      initial_material = button.GetComponent<Renderer>().material;
    }

    void OnTriggerStay(Collider other){
      if (nooted.isStretching && other.tag == "Player"){
        if (!cooldown) {
          other.transform.parent = trigger.transform;
          button.GetComponent<Renderer>().material = red_mat;
          trigger.GetComponent<SkyWalk>().MachineGo();
          cooldown = true;
          StartCoroutine(Delay());
        }
      }
    }

    IEnumerator Delay()
    {
      yield return new WaitForSeconds(2.0f);
      trigger.GetComponent<SkyWalk>().elevator_anim.SetBool("isMove", false);
      yield return new WaitForSeconds(40.0f);
      button.GetComponent<Renderer>().material = initial_material;
      cooldown = false;
    }

}
