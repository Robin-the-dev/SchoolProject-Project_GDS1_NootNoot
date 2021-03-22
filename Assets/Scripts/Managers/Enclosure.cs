using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class Enclosure : MonoBehaviour
{
    public enum EnclosureAnimal
    {
        Penguin, Monkey, Lion, Parrot
    };

    [SerializeField] private EnclosureAnimal enclosureAnimal = EnclosureAnimal.Penguin;
    [SerializeField] private AmbienceSource[] ambienceSources;
    private bool activate;// Start is called before the first frame update

    private float delay = 1f;
    [SerializeField] private float minDelay, maxDelay;
    
 
    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            if (delay < 0)
            {
                PlaySound();
                delay = Random.Range(minDelay, maxDelay);
            }
        
            delay -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activate = true;
            ZoneManager.Instance.UpdateCurrentEnclosure(enclosureAnimal);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activate = false;
        }
    }

    private void PlaySound()
    {
        if (ambienceSources.Length == 0) return;
        var penguinID = Random.Range(0, ambienceSources.Length);
        ambienceSources[penguinID].PlaySound();
        //Debug.Log("Play ambience");
    }
}
