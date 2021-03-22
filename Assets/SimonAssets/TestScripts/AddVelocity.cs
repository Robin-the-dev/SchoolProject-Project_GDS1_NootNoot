using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddVelocity : MonoBehaviour
{
  public float max_vel;
  public bool tap = false;
  private Rigidbody hint_rb;
  public float direction;
  private AudioSource audioSource;
  
  // Start is called before the first frame update
    void Start()
    {
      hint_rb = GetComponent<Rigidbody>();
      hint_rb.maxAngularVelocity = max_vel;
      audioSource = GetComponent<AudioSource>();
    }

    //Every time a hint/pebble spawns, when it hits the glass, it will play the-
    //-tap sound once, then the tap bool will equal true, not allowing that-
    //-instance of a rock to play sound. This sound is only active when the lion puzzle is not complete.
    void OnCollisionEnter(Collision other){

      if (other.transform.gameObject.tag == "Window" && !tap && !GameManager.Instance.GetLionPuzzle() ){

        //Plays audio
        audioSource.Play();

        tap = true;
        
      }
      if (other.transform.gameObject.tag == "Ramp")
        hint_rb.velocity = hint_rb.velocity + new Vector3(1f,0.0f,-direction)* 1.5f;
    }


}
