using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    [SerializeField] private AudioMixerSnapshot SnapshotUnderwater, SnapshotNormal;

    [SerializeField] private AudioMixer masterMixer;

    private float originalMasterVol; 
    private enum snapshotState{Normal, UnderWater};

    private snapshotState state;

    // Update is called once per frame
    public void MusicUnderwater()
    {
        SnapshotUnderwater.TransitionTo(0.1f);
        state = snapshotState.UnderWater;
    }
    
    public void MusicNotUnderwater()
    {
        SnapshotNormal.TransitionTo(0.1f);
        state = snapshotState.Normal;
    }

    public void LowerMaster()
    {
        masterMixer.GetFloat("MasterVol", out originalMasterVol);
        masterMixer.SetFloat("MasterVol", originalMasterVol -5f);
    }
    
    public void RaiseMaster()
    {
        masterMixer.GetFloat("MasterVol", out originalMasterVol);
        masterMixer.SetFloat("MasterVol", originalMasterVol +5f);
    }

    public void ResetSnapshot()
    {
        if (state != snapshotState.Normal)
        {
            MusicNotUnderwater();
        }
    }
}
