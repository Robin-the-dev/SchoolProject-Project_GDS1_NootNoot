using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Experimental.UIElements;

public class LionAudioController : EnclosureAudioController
{
    protected override void ActivateAudio()
    {
        base.ActivateAudio();
        
        //Fades out cafe vol
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "CafeVol", fadeDuration, 0.2f));
    }

    protected override void DeactivateAudio()
    {
        base.DeactivateAudio();
        
        //Fades in cafe vol
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "CafeVol", fadeDuration, 1f));
    }
}
