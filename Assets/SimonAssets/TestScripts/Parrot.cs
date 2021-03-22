using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrot : MonoBehaviour
{
  public static bool onShoulder = false;
  private Rigidbody parrot_rb;
  private Collider parrot_col;
  private Collider player_col;
  private Vector3 inital_pos;
  private Quaternion inital_rot;
  public bool parrot_puzzle = false;

  //[SerializeField] private ParrotLookOutside parrotLookOutside;

  [SerializeField] private ParrotLookClose parrotLookClose;
    // Start is called before the first frame update
    void Start()
    {
      parrot_rb = GetComponent<Rigidbody>();
      parrot_col = GetComponent<Collider>();
      inital_pos = transform.position;
      inital_rot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
      //If the player is captured by guard, the parrot's falls to the ground
      if (!onShoulder && parrot_rb.isKinematic){
        Physics.IgnoreCollision(player_col, parrot_col, true);
        transform.parent = null;
        parrot_col.isTrigger = false;
        parrot_rb.isKinematic = false;
        StartCoroutine(Dropped());
      }
    }

    void OnCollisionEnter(Collision other){
      if (other.transform.gameObject.tag == "Player"){
        player_col = other.collider;
        parrot_rb.isKinematic = true;
        parrot_col.isTrigger = true;
        transform.parent = other.transform;
        transform.localPosition = Vector3.zero + new Vector3(0.6f,1.8f,0.0f);
        transform.rotation = other.transform.rotation;
        onShoulder = true;
      }

    }

    void OnTriggerEnter(Collider other){
      if (other.tag == "Rescued" && !parrot_puzzle){
        Physics.IgnoreCollision(player_col, parrot_col, true);
        transform.parent = null;
        parrot_col.isTrigger = false;
        parrot_rb.isKinematic = false;
        parrotLookClose.Activate();
        StartCoroutine(Rescued());
      }
    }

    //after 3 seconds the parrot is reset back to the inital position, waiting to be rescued again
    IEnumerator Dropped(){
      yield return new WaitForSeconds(3.0f);
      Physics.IgnoreCollision(player_col, parrot_col, false);
      transform.position = inital_pos;
      transform.rotation = inital_rot;
    }
    IEnumerator Rescued(){
      yield return new WaitForSeconds(6.0f);
      parrotLookClose.Complete();
      Physics.IgnoreCollision(player_col, parrot_col, false);
      parrot_puzzle = true;
      //parrotLookOutside.Complete();
      GameManager.Instance.ParrotPuzzle();
    }

}
