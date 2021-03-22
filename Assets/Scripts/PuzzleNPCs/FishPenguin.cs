using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPenguin : MonoBehaviour
{
    public GameObject[] fishes;
    [SerializeField] private GameObject penguinCanvas, fishIcon, smile;
    private bool finished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!finished)
            {
                if (other.gameObject.GetComponent<Player>().UseItem(CollectItem.Item.Fish))
                {
                    //Finished and disables banana and enables smile
                    finished = true;
                    fishIcon.SetActive(false);
                    smile.SetActive(true);
                    foreach (var fish in fishes)
                    {
                        Destroy(fish);
                    }
                    GameManager.Instance.FishPuzzle();
                    /*player_banana.transform.parent = transform;
                    player_banana.transform.localPosition = new Vector3(-0.318f,0.912f,0.189f);*/
                }
            }
            //Turns the thought bubble on
            //Need lean tween animations
            penguinCanvas.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Causes the parent to face the player
        if (other.gameObject.CompareTag("Player"))
        {
            var parent = transform.parent.transform;
            Vector3 targetPosition = new Vector3(other.transform.position.x, parent.position.y, other.transform.position.z);
            parent.LookAt( targetPosition, Vector3.up) ;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Disables canvas
        if (other.gameObject.CompareTag("Player"))
        {
            penguinCanvas.gameObject.SetActive(false);
        }
    }
}
