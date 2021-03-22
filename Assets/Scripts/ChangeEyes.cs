using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEyes : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] eyes;
    [SerializeField] private Material eyesMat;

    [SerializeField] private ParticleSystem[] particleSystems;

    public void Activate()
    {
        foreach (var eye in eyes)
        {
            eye.material = eyesMat;
        }

        foreach (var system in particleSystems)
        {
            system.gameObject.SetActive(true);
        }
    }
}
