using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    public GameObject player_banana;
    [SerializeField] private GameObject monkeyCanvas, banana, smile;
    private bool finished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!finished)
            {
                if (other.gameObject.GetComponent<Player>().UseItem(CollectItem.Item.Banana))
                {
                    //Finished and disables banana and enables smile
                    finished = true;
                    banana.SetActive(false);
                    smile.SetActive(true);
                    GameManager.Instance.MonkeyPuzzle();
                    player_banana.transform.parent = transform;
                    player_banana.transform.localPosition = new Vector3(-0.318f,0.912f,0.189f);
                }
            }
            //Turns the thought bubble on
            //Need lean tween animations
            monkeyCanvas.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Causes the parent to face the player
        if (other.gameObject.CompareTag("Player"))
        {
            Transform parent = transform.parent.transform;
            Vector3 targetPosition = new Vector3(other.transform.position.x, parent.position.y, other.transform.position.z);
            parent.LookAt( targetPosition, Vector3.up) ;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Disables canvas
        if (other.gameObject.CompareTag("Player"))
        {
            monkeyCanvas.gameObject.SetActive(false);
        }
    }
}
