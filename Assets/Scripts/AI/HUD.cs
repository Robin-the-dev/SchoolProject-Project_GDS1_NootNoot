using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void FixedUpdate()
    {
        transform.LookAt(mainCam.transform.position);
        transform.Rotate(0f, 180, 0f);
    }
}
