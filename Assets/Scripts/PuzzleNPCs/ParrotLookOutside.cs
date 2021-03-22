using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotLookOutside : MonoBehaviour
{
    [SerializeField] private GameObject parrotCanvas, helpIcon;
    [SerializeField] private GameObject cockatiel;
    private bool finished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Turns the thought bubble on
            //Need lean tween animations
            helpIcon.SetActive(true);
            parrotCanvas.SetActive(true);
        }
    }private void OnTriggerStay(Collider other)
         {
             //Causes the parent to face the player
             if (other.gameObject.CompareTag("Player"))
             {
                 Transform parent = transform.parent.transform;
                 Vector3 targetPosition = new Vector3(other.transform.position.x, cockatiel.transform.position.y, other.transform.position.z);
                 cockatiel.transform.LookAt(targetPosition, Vector3.up) ;
             }
         }
    
    private void OnTriggerExit(Collider other)
    {
        //Disables canvas
        if (other.gameObject.CompareTag("Player"))
        {
            helpIcon.SetActive(false);
            parrotCanvas.gameObject.SetActive(false);
        }
    }

    public void Complete()
    {
        Destroy(gameObject);
    }
}
