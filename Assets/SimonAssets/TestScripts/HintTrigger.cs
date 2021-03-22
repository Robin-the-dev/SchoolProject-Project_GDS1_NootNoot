using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject hint;
    private GameObject new_hint;
    private bool cooldown = false;

    //When the bird hits the trigger(this object) spawn a hint/rock, and start a cooldown
    void OnTriggerExit(Collider other){
      if (other.tag != "Player" && !cooldown){
        //Debug.Log("hint");
        new_hint = Instantiate(hint, transform.position, Quaternion.identity);
        Physics.IgnoreCollision(player.GetComponent<Collider>(), new_hint.GetComponent<Collider>(), true);
        StartCoroutine(Despawn());
        cooldown = true;
      }
    }

    IEnumerator Despawn(){
      yield return new WaitForSeconds(2.0f);
         cooldown = false;
      yield return new WaitForSeconds(15.0f);
      Destroy(new_hint);
    }
}
