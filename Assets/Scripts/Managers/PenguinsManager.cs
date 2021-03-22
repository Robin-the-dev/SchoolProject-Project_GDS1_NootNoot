using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinsManager : MonoBehaviour
{
    [SerializeField] private BoxCollider[] penguins;

    private void Start()
    {
        penguins = GetComponentsInChildren<BoxCollider>();
    }

    public void FishPuzzleComplete()
    {
        foreach (var penguin in penguins)
        {
            penguin.enabled = false;
        }
    }
}
