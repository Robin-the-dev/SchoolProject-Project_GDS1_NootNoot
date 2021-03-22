using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEyesLion : MonoBehaviour
{
    private MeshRenderer eyes;
    private Material[] eyesMat;
    [SerializeField] private Material greenEyes;

    [SerializeField] private ParticleSystem[] particleSystems;

    private void Start()
    {
        eyes = GetComponent<MeshRenderer>();
        eyesMat = eyes.materials;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Activate();
        }
    }

    public void Activate()
    {
        eyesMat[2] = greenEyes;
        eyes.materials = eyesMat;

        foreach (var system in particleSystems)
        {
            system.gameObject.SetActive(true);
        }
    }
}
