using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class AmbienceSource : MonoBehaviour
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
        var clipID = Random.Range(0, audioClips.Length);
        try
        {
            var clip = audioClips[clipID];
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
            Debug.Log(clipID + " Not found on " + gameObject.name);
            throw;
        }
    }
}
