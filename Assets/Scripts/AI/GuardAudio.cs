using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
 * Plays audio for the guard. State is case sensitive
 */
public class GuardAudio : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip alert, chase, confused, captured;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(String state)
    {
        switch (state)
        {
            case "Alert":
                audioSource.clip = alert;
                break;
            case "Chasing":
                audioSource.clip = chase;
                break;
            case "Confused":
                audioSource.clip = confused;
                break;
            case "Captured":
                audioSource.clip = captured;
                break;
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.Play();
    }
}
