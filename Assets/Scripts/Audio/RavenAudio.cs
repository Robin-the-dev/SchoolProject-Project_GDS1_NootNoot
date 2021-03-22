using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RavenAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    public void PlaySound()
    {
        //Picks a random clip and tries to play it else output error
        var clipId = Random.Range(0, audioClips.Length);
        try
        {
            var clip = audioClips[clipId];
            if (audioSource.clip != clip)
            {
                audioSource.clip = clip;
            }

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Debug.Log(clipId + " Not found on " + gameObject.name);
            throw;
        }
    }
}
