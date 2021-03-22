using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource playerSource;

    //Variables for SFX clips
    [Header("SFX Clips")] 
    [SerializeField] private AudioClip[] player_Honk;

    [SerializeField] private AudioClip vault_Fail, vault_Success;

    [SerializeField] private float honkRadius;

    private Collider[] children = new Collider[0];

    private int[] sequence = {0, 1, 2, 0, 1, 0, 2, 0, 1};

    private int number = 0;
    //[SerializeField] private AudioClip player_Honk2, player_Walk;
    
    // Start is called before the first frame update
    void Start()
    {
        playerSource = GetComponent<AudioSource>();
    }
    
    public void Honk()
    {
        //If audio isn't playing then honk
        if (playerSource.isPlaying) return;
        //var clip = Random.Range(0, player_Honk.Length); //Pick randomly in the list
        
        //If current clip isn't new clip set it as the new clip
        if (playerSource.clip != player_Honk[sequence[number]])
        {
            playerSource.clip = player_Honk[sequence[number]];
        }
        
        number++;
        if (number > sequence.Length - 1)
        {
            number = 0;
        }

        playerSource.Play();
            
        //Creates a sphere and find all colliders within it under child
        children = Physics.OverlapSphere(transform.position, honkRadius, LayerMask.GetMask("Child"));
        foreach (var child in children)
        {
            //If there is a child activate it (i.e. drop a banana)
            if (child)
            {
                child.GetComponent<ChildWithBanana>().Activate();
                //Debug.Log(child);
            }
        }
    }

    public void PlayVaultFail()
    {
        //If audio isn't playing then honk
        if (playerSource.isPlaying) return;
        
        if (playerSource.clip != vault_Fail)
        {
            playerSource.clip = vault_Fail;
        }

        playerSource.Play();
    }

    public void PlayVaultSuccess()
    {
        //If audio isn't playing then honk
        if (playerSource.isPlaying) return;
        
        if (playerSource.clip != vault_Success)
        {
            playerSource.clip = vault_Success;
        }
        playerSource.Play();
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, honkRadius);
    }
    
}
