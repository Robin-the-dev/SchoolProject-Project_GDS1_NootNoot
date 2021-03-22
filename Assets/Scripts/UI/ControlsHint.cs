using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsHint : MonoBehaviour
{
    public enum Tutorial {MoveTut, RMBTut, HonkTut,  SpaceTut, None};

    [SerializeField] private Tutorial tutorial;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().ActivateTutorial(tutorial);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().DeactivateControlsTutorial(tutorial);
        }
    }

    public void PuzzleComplete()
    {
        gameObject.SetActive(false);
    }
}
