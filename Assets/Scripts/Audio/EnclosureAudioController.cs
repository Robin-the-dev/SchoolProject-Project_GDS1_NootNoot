using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnclosureAudioController : MonoBehaviour
{
    [SerializeField] protected List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField] protected AudioMixer audioMixer;
    [SerializeField] protected string param;

    [SerializeField] protected float fadeDuration;
    // Start is called before the first frame update
    void Start()
    {
        //Grabs all audio sources attached to object and puts it in the list 
        var asList = GetComponents<AudioSource>();
        foreach (var audioSource in asList)
        {
            audioSources.Add(audioSource);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateAudio();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DeactivateAudio();
        }
    }

    protected virtual void ActivateAudio()
    {
        //Plays all audio in the list
        foreach (var audioSource in audioSources)
        {
            audioSource.Play();
        }
            
        //Fades in audio
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, param, fadeDuration, 1));
    }

    protected virtual void DeactivateAudio()
    {
        //Fades Out audio
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, param, fadeDuration, 0));
        StartCoroutine(Stop());
    }

    private IEnumerator Stop()
    {
        //Waits for fade then stops audio sources
        yield return new WaitForSeconds(fadeDuration);
        foreach (var audioSource in audioSources)
        {
            audioSource.Stop();
        }
    }
}
