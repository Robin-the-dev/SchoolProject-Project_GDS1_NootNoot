using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatScripts : MonoBehaviour
{
  //Variables
  public GameObject[] hats;
  private List<string> hat_list = new List<string>();
  private GameObject new_hat;
  private Vector3[] hat_pos = new [] {
    new Vector3(0.069f,2.552f,0.0016f), //Spinning hat pos
    new Vector3(0.054f,2.213f,0.036f), //Joe Rogan hat pos
    new Vector3(0.029f,2.007f,0.0f), //Magican's hat
    new Vector3(0.052f,2.52f,0.013f),//Chef's Hat
    new Vector3(0.053f,2.092f,0.0f), //Guard's hat
    new Vector3(0.053f,2.092f,0.0f) //Comrade Noots
  };
  private Vector3[] hat_rot = new [] {
    new Vector3(0.0f, -102.477f, 0.0f),
    new Vector3(0.0f,-2.531f,0.0f),
    new Vector3(0.0f,-90.0f, 0.0f),
    new Vector3(0.0f,0.0f,0.0f),
    new Vector3(-75.7930f,180.0f,90.00001f),
    new Vector3(0.053f,2.092f,0.0f)
  };

  private bool hat_on = false;

  public delegate void HatPickedUp(int num);

  public static HatPickedUp hatPickedUp;

  void Start(){
    hat_list.Add("placeholder");
    hat_list.Add("placeholder");
    hat_list.Add("placeholder");
    hat_list.Add("placeholder");
    hat_list.Add("placeholder");
    hat_list.Add("placeholder");
  }


  //Hat Key
  //0: Spinning Hat
  //1: Joe Rogan
  //2: Magican's Hat
  //3: Chef's Hat
  //4: Guard's hat
  //5: ComradeNoots

  void Update(){
    if (hat_on && hat_list.Count >= 2){
      if (Input.GetKeyDown(KeyCode.Alpha1) && hat_list.Contains(hats[0].name)){
        Destroy(new_hat);
        new_hat = Instantiate(hats[0], hat_pos[0], transform.rotation, transform);
        new_hat.GetComponent<Collider>().enabled = false;
        new_hat.GetComponent<Rigidbody>().isKinematic = true;
        new_hat.transform.parent = transform;
        new_hat.transform.localPosition = Vector3.zero + hat_pos[0];
        new_hat.transform.localRotation = Quaternion.Euler(hat_rot[0]);
      }
      if (Input.GetKeyDown(KeyCode.Alpha2) && hat_list.Contains(hats[1].name)){
        Destroy(new_hat);
        new_hat = Instantiate(hats[1], hat_pos[1], transform.rotation, transform);
        new_hat.GetComponent<Collider>().enabled = false;
        new_hat.GetComponent<Rigidbody>().isKinematic = true;
        new_hat.transform.parent = transform;
        new_hat.transform.localPosition = Vector3.zero + hat_pos[1];
        new_hat.transform.localRotation = Quaternion.Euler(hat_rot[1]);
      }
      if (Input.GetKeyDown(KeyCode.Alpha3) && hat_list.Contains(hats[2].name)){
        Destroy(new_hat);
        new_hat = Instantiate(hats[2], hat_pos[2], transform.rotation, transform);
        new_hat.GetComponent<Collider>().enabled = false;
        new_hat.GetComponent<Rigidbody>().isKinematic = true;
        new_hat.transform.parent = transform;
        new_hat.transform.localPosition = Vector3.zero + hat_pos[2];
        new_hat.transform.localRotation = Quaternion.Euler(hat_rot[2]);
      }
      if (Input.GetKeyDown(KeyCode.Alpha4) && hat_list.Contains(hats[3].name)){
        Destroy(new_hat);
        new_hat = Instantiate(hats[3], hat_pos[3], transform.rotation, transform);
        new_hat.GetComponent<Collider>().enabled = false;
        new_hat.GetComponent<Rigidbody>().isKinematic = true;
        new_hat.transform.parent = transform;
        new_hat.transform.localPosition = Vector3.zero + hat_pos[3];
        new_hat.transform.localRotation = Quaternion.Euler(hat_rot[3]);
      }
      if (Input.GetKeyDown(KeyCode.Alpha5) && hat_list.Contains(hats[4].name)){
        Destroy(new_hat);
        new_hat = Instantiate(hats[4], hat_pos[4], transform.rotation, transform);
        new_hat.GetComponent<Collider>().enabled = false;
        new_hat.GetComponent<Rigidbody>().isKinematic = true;
        new_hat.transform.parent = transform;
        new_hat.transform.localPosition = Vector3.zero + hat_pos[4];
        new_hat.transform.localRotation = Quaternion.Euler(hat_rot[4]);
      }
      if (Input.GetKeyDown(KeyCode.Alpha6) && hat_list.Contains(hats[5].name)){
        Destroy(new_hat);
        new_hat = Instantiate(hats[5], hat_pos[5], transform.rotation, transform);
        new_hat.GetComponent<Collider>().enabled = false;
        new_hat.GetComponent<Rigidbody>().isKinematic = true;
        new_hat.transform.parent = transform;
        new_hat.transform.localPosition = Vector3.zero + hat_pos[5];
        new_hat.transform.localRotation = Quaternion.Euler(hat_rot[5]);
      }
    }
    //if (Input.GetKeyDown(KeyCode.Alpha0))
    //  Debug.Log(hat_list.Count);
  }


    void OnCollisionEnter(Collision other){
      if (other.transform.gameObject.tag == "Hat" && !hat_on){
        //spinning hat
        int i = 10;// 10 is just so the int can be initalized, it doesnt mean anything,
        //only when its values 1-3 does it become important to change the hats position for the first hat
        if (other.transform.gameObject.name == hats[0].name){
          hat_list[0] =other.transform.gameObject.name;
          i = 0;
        }
        //The Joe
        if (other.transform.gameObject.name == hats[1].name){
          hat_list[1] = other.transform.gameObject.name;
          i = 1;
        }
        //Magic Hat
        if (other.transform.gameObject.name == hats[2].name) {
          hat_list[2] = other.transform.gameObject.name;
          i = 2;
        }
        //Chef's Hat
        if (other.transform.gameObject.name == hats[3].name){
          hat_list[3] = other.transform.gameObject.name;
          i = 3;
        }
        //Guard Hat
        if (other.transform.gameObject.name == hats[4].name)
        {
          hat_list[4] = other.transform.gameObject.name;
          i = 4;
        }
        //Comrade Noots
        if (other.transform.gameObject.name == hats[5].name)
        {
          hat_list[5] = other.transform.gameObject.name;
          i = 5;
        }

        hat_on = true;
        other.collider.enabled = false;
        other.rigidbody.isKinematic = true;
        other.transform.parent = transform;
        other.transform.localPosition = hat_pos[i];
        other.transform.localRotation = Quaternion.Euler(hat_rot[i]);
        new_hat = other.transform.gameObject;
        
        hatPickedUp?.Invoke(i);
      }
      else if (other.transform.gameObject.tag == "Hat" && hat_on){

        if (other.transform.gameObject.name == hats[0].name)
          hat_list[0] = other.transform.gameObject.name;
        if (other.transform.gameObject.name == hats[1].name)
          hat_list[1] = other.transform.gameObject.name;
        if (other.transform.gameObject.name == hats[2].name)
          hat_list[2] = other.transform.gameObject.name;
        if (other.transform.gameObject.name == hats[3].name)
          hat_list[3] = other.transform.gameObject.name;
        if (other.transform.gameObject.name == hats[4].name)
          hat_list[4] = other.transform.gameObject.name;
        if (other.transform.gameObject.name == hats[5].name)
          hat_list[5] = other.transform.gameObject.name;
        Destroy(new_hat);

        if (other.transform.gameObject.name == hats[0].name){
          other.collider.enabled = false;
          other.rigidbody.isKinematic = true;
          other.transform.parent = transform;
          other.transform.localPosition = Vector3.zero + hat_pos[0];
          other.transform.localRotation = Quaternion.Euler(hat_rot[0]);
          new_hat = other.transform.gameObject;
          hatPickedUp?.Invoke(0);
        }
        if (other.transform.gameObject.name == hats[1].name){
          other.collider.enabled = false;
          other.rigidbody.isKinematic = true;
          other.transform.parent = transform;
          other.transform.localPosition = Vector3.zero + hat_pos[1];
          other.transform.localRotation = Quaternion.Euler(hat_rot[1]);
          new_hat = other.transform.gameObject;
          hatPickedUp?.Invoke(1);
        }
        if (other.transform.gameObject.name == hats[2].name){
          other.collider.enabled = false;
          other.rigidbody.isKinematic = true;
          other.transform.parent = transform;
          other.transform.localPosition = Vector3.zero + hat_pos[2];
          other.transform.localRotation = Quaternion.Euler(hat_rot[2]);
          new_hat = other.transform.gameObject;
          hatPickedUp?.Invoke(2);
        }
        if (other.transform.gameObject.name == hats[3].name){
          other.collider.enabled = false;
          other.rigidbody.isKinematic = true;
          other.transform.parent = transform;
          other.transform.localPosition = Vector3.zero + hat_pos[3];
          other.transform.localRotation = Quaternion.Euler(hat_rot[3]);
          new_hat = other.transform.gameObject;
          hatPickedUp?.Invoke(3);
        }
        if (other.transform.gameObject.name == hats[4].name){
          other.collider.enabled = false;
          other.rigidbody.isKinematic = true;
          other.transform.parent = transform;
          other.transform.localPosition = Vector3.zero + hat_pos[4];
          other.transform.localRotation = Quaternion.Euler(hat_rot[4]);
          new_hat = other.transform.gameObject;
          hatPickedUp?.Invoke(4);
        }
        if (other.transform.gameObject.name == hats[5].name){
          other.collider.enabled = false;
          other.rigidbody.isKinematic = true;
          other.transform.parent = transform;
          other.transform.localPosition = Vector3.zero + hat_pos[5];
          other.transform.localRotation = Quaternion.Euler(hat_rot[5]);
          new_hat = other.transform.gameObject;
          hatPickedUp?.Invoke(5);
        }

      }

    }
}
