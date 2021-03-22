using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPoleVault : MonoBehaviour
{
  public bool isGrounded = false;
  public bool isVaulting = false;
  [SerializeField]
  private GameObject anchor;
  public GameObject beak;
  private Vault headhinge_script;

  private PlayerAudio playerAudio;
  private Rigidbody rbd;

  void Start()
  {
    headhinge_script = GetComponent<Vault>();
    playerAudio = GetComponent<PlayerAudio>();
    rbd = GetComponent<Rigidbody>();
  }

  void Update()
  {
    if(isVaulting)
        rbd.useGravity = false;

    //If the Player is on the ground (not in water), and would like to pole vault
    if (!Water.isWater){
      if (Input.GetKeyDown(KeyCode.Space) && isGrounded && anchor.transform.localScale.z == 1){
                rbd.useGravity = true;

        RaycastHit hit_object;
        beak.GetComponent<Collider>().enabled = false;
        if (Physics.Raycast(transform.position + new Vector3(0.0f,1.9f,0.0f), transform.TransformDirection(Vector3.forward), out hit_object, 6.0f)){
          Debug.DrawLine(transform.position + new Vector3(0.0f,1.9f,0.0f), hit_object.point, Color.red, 10.0f);

          //Plays fail audio
          playerAudio.PlayVaultFail();
          //Debug.Log("Hit Wall, play fail sound");
        }
        else{
          //Debug.Log("Initiate Pole Vault Mode");
          isVaulting = true;
          isGrounded = false;
          beak.SetActive(false);

          //Plays success audio
          playerAudio.PlayVaultSuccess();

          //This script spawns a hinge joint to the head and a pole attached to the joint.
          headhinge_script.PoleSpawn();
        }
        beak.GetComponent<Collider>().enabled = true;

        //StartCoroutine(GroundAgain());
      }
      //If the player is vaulting, and would like to cancel vault
      // else if (isVaulting && !isGrounded && Input.GetKeyDown(KeyCode.Space)){
      //   Debug.Log("Pressed Space to cancel vault");
      //   isVaulting = false;
      //   beak.SetActive(true);
      //   //Destroy the pole and joint so it resets for the next spawn
      //   Destroy(head.GetComponent<HingeJoint>());
      //   GameObject[] poles = GameObject.FindGameObjectsWithTag("Pole");
      //   foreach(GameObject p in poles){
      //     Destroy(p);
      //   }
      //
      // }
      //If the Player has succesfully vaulted and landed onto a higher ground. (or the floor itself??)
      if (isVaulting && isGrounded){
        //Debug.Log("Landed on floor/obstacle");
        isVaulting = false;
        Destroy(GetComponent<HingeJoint>());
        GameObject[] poles = GameObject.FindGameObjectsWithTag("Pole");
        foreach(GameObject p in poles){
          Destroy(p);
        }
        beak.SetActive(true);
      }
    }
    else{
      isVaulting = false;
      Destroy(GetComponent<HingeJoint>());
      GameObject[] poles = GameObject.FindGameObjectsWithTag("Pole");
      foreach(GameObject p in poles){
        Destroy(p);
      }
      beak.SetActive(true);
    }

    if(isGrounded){
      headhinge_script.hasJoint = false;
    }
  }

  IEnumerator GroundAgain(){
    if (!isGrounded)
      yield return new WaitForSeconds(3.0f);
    if (!isGrounded){
      isGrounded = true;
      Debug.Log("Force Cancel Vault");
    }
  }

}
