using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CentralZooAudioController : MonoBehaviour
{
    private AudioSource[] audioSources;

    [SerializeField] private AudioSource fountain;

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private string param;

    [SerializeField] private float fadeDuration;
    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        foreach (var audioSource in audioSources)
        {
            audioSource.Play();
        }
        fountain.Play();
        
        //Fades in audio
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, param, fadeDuration, 1));
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        //Fades out audio
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, param, fadeDuration, 0));
        StartCoroutine(Stop());
    }
    private IEnumerator Stop() 
    { 
        yield return new WaitForSeconds(fadeDuration);
        foreach (var audioSource in audioSources)
        {
            audioSource.Stop();
        }
        fountain.Stop();
    }
}

