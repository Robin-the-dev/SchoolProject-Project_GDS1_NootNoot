using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTest : MonoBehaviour
{
    public AudioClip test, underwater;
    public AudioMixer master;
    public AudioMixerSnapshot snapshot_Normal, snapshot_Underwater;
    public int snapshotState = 0;

    private AudioSource playerSource;
    // Start is called before the first frame update
    void Start()
    {
        playerSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!playerSource.isPlaying)
            {
                playerSource.PlayOneShot(test);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            snapshotState = snapshotState == 0 ? 1 : 0; //Sets state to opposite of what it is
                switch (snapshotState)
                {
                    case 0: snapshot_Normal.TransitionTo(0.1f);
                        break;
                    case 1: snapshot_Underwater.TransitionTo(0.1f);
                        break;
                }
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!playerSource.isPlaying)
            {
                master.ClearFloat("MusicVol");
                master.ClearFloat("SFXVol");
            }
        }
    }
}
