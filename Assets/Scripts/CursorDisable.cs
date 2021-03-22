using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is script for Cursor disabled. You should use cmd + p, or ctrl + p to play and stop unity game!!!
public class CursorDisable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
