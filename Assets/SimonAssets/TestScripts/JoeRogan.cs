using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeRogan : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audio;
    int i = 0;
    
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.gameObject.tag == "Player" && i <= 3)
        {
            audio.clip = clips[i];
            audio.Play();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            audio.Stop();
            i++;
            if (i >= 4)
            {
                i = 0;
            }
        }
    }
}
