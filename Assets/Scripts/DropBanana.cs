using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBanana : MonoBehaviour
{
    [SerializeField] private GameObject banana;

    [SerializeField] private Transform bananaSpawn;

    private bool triggered;
 
    /*
     * Call this function to create banana
     */
    private void SpawnBanana()
    {
        if (!triggered)
        {
            Instantiate(banana, bananaSpawn.position, Quaternion.identity);
            triggered = true;
        }
    }
}
