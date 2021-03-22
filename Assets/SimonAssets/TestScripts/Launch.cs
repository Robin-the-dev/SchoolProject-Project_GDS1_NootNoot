using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public bool col1;
    public GameObject[] cams;
    private Rigidbody rb;
    private bool hit = true;
    public BlurPanel endcard;
    public BlurPanel fade_to_black;
    void OnTriggerEnter(Collider other)
    {
        if (!col1)
        {
            cams[0].SetActive(false);
            cams[1].SetActive(false);
            cams[2].SetActive(true);
            rb = other.attachedRigidbody;
            rb.velocity *= speed;
            StartCoroutine(Slow());
        }
        if (col1)
        {
            player.GetComponent<PlayerInput>().enabled = false;
        }
        
    }

    IEnumerator Slow()
    {
        yield return new WaitForSeconds(6f);
        hit = false;
        rb.velocity *= 0;
        endcard.gameObject.SetActive(true);
        endcard.Activate();
        yield return new WaitForSeconds(4f);
        fade_to_black.gameObject.SetActive(true);
        fade_to_black.Activate();
        yield return new WaitForSeconds(2.5f);
        GameManager.Instance.LevelComplete();
    }
}
