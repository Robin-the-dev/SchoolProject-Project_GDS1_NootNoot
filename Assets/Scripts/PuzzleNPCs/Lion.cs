using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion : MonoBehaviour
{
    [SerializeField] private GameObject lionCanvas;
    [SerializeField] private Animator animator;
    private bool finished;
    private static readonly int Saved = Animator.StringToHash("Saved");

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!finished)
            {
                //Finished enables smile
                finished = true;
                animator.SetTrigger(Saved);
                GameManager.Instance.LionPuzzle();
                
            }
            //Turns the thought bubble on
            //Need lean tween animations
            lionCanvas.SetActive(true);
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
            lionCanvas.gameObject.SetActive(false);
        }
    }
}
