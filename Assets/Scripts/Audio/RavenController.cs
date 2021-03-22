using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RavenController : MonoBehaviour
{
    [SerializeField] private float minDelay, maxDelay;
    [FormerlySerializedAs("ambienceSources")] [SerializeField] private RavenAudio[] ravenAudios;
    private float delay;
    
    // Start is called before the first frame update
    void Start()
    {
        delay = Random.Range(minDelay, maxDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (delay < 0)
        {
            PlaySound();
            delay = Random.Range(minDelay, maxDelay);
        }
        
        delay -= Time.deltaTime;
    }
    
    private void PlaySound()
    {
        if (ravenAudios.Length == 0) return;
        var ravenId = Random.Range(0, ravenAudios.Length);
        ravenAudios[ravenId].PlaySound();
        //Debug.Log("Play ambience");
    }
}
