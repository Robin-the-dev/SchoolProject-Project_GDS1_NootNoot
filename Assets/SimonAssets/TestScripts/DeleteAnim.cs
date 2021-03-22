using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAnim : MonoBehaviour
{
    public void SelfDestruct()
    {
        Destroy(GetComponent<Animator>());
    }
}
