using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotLookClose : MonoBehaviour
{
    [SerializeField] private GameObject parrotCanvas, smile;
    private bool activateIcon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Turns the thought bubble on
            //Need lean tween animations
            if (activateIcon)
            {
                smile.SetActive(true);
                parrotCanvas.SetActive(true);
            }
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
            smile.SetActive(false);
            parrotCanvas.gameObject.SetActive(false);
        }
    }

    public void Activate()
    {
        activateIcon = true;
    }

    public void Complete()
    {
        parrotCanvas.SetActive(false);
        Destroy(gameObject);
    }
}