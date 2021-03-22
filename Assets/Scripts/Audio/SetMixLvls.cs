using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetMixLvls : MonoBehaviour
{
    // Controls the volume lvls. Used with a slider
    public AudioMixer masterMixer;

    public void SetSFXLvl(float sfxLvl)
    {
        masterMixer.SetFloat("SFXVol", sfxLvl);
    }

    public void SetMusicLvl(float musicLvl)
    {
        masterMixer.SetFloat("MusicVol", musicLvl);
    }
    public void SetMasterLvl(float masterLvl)
    {
        masterMixer.SetFloat("MasterVol", masterLvl);
    }
    public void SetAmbienceLvl(float ambienceLvl)
    {
        masterMixer.SetFloat("AmbienceVol", ambienceLvl);
    }
}
