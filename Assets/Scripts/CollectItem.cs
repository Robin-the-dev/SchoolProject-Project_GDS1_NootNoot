using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Set item of the object to be collected. Triggers on collision and player gains item.
 */
public class CollectItem : MonoBehaviour
{
    public enum Item {Fish, Banana};
    private Vector3 banana_pos;
    private Vector3 fish_pos;
    [SerializeField] private Item item;
    private int index = 0;

    void Start(){
      banana_pos = new Vector3(0.0f,2.4f,0.0f);
      fish_pos = new Vector3(0.0f,2.6f,0.0f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().CollectItem(item);
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            if(item == Item.Banana)
              transform.position = other.gameObject.transform.position + banana_pos;

            if(item == Item.Fish){

              foreach(Transform child in other.transform)
              {
                if(child.tag == "Fish")
                  index++;
              }

              if (index == 0){
                transform.position = other.gameObject.transform.localPosition + fish_pos;
              }
              else if (index == 1){
                transform.position = other.gameObject.transform.localPosition + (fish_pos + new Vector3(0.0f,0.66f,0.0f) );
              }
              else if (index == 2){
                transform.position = other.gameObject.transform.localPosition + (fish_pos + new Vector3(0.0f, 0.66f*2f,0.0f) );
              }

            }
            transform.rotation = other.transform.rotation;
            transform.parent = other.gameObject.transform;
        }
    }


}
