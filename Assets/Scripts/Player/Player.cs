using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    private int hasFish = 0;
    private bool hasBanana;
    
    //Things for fishIcon and bananaIcon.obsolete
    /*[SerializeField] private GameObject fishIcon, bananaIcon;*/
    
    private List<GameObject> fishes = new List<GameObject>();
    
    //Bools set if tuts are finished
    private bool moveTutFin, honkTutFin, RMBTutFin, spaceTutFin;
    

    //If all tuts are completed
    private bool allTutsCompleted;
    
    //Icons for the controls
    [SerializeField] private GameObject moveTutIcon, honkTutIcon, RMBTutIcon, spaceTutIcon, tutCanvas;

    private PlayerInput playerInput;
    
    [SerializeField] private ControlsHint.Tutorial tutorialInProgress = ControlsHint.Tutorial.MoveTut;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (tutorialInProgress != ControlsHint.Tutorial.None)
        {
            switch (tutorialInProgress)
            {
                case ControlsHint.Tutorial.MoveTut:
                    if (playerInput.moveInput != Vector2.zero)
                    {
                        //DeactivateControlsTutorial(tutorialInProgress);
                        moveTutFin = true;
                        StartCoroutine(ActivateWithDelay(ControlsHint.Tutorial.RMBTut));
                    }
                    break;
                case ControlsHint.Tutorial.RMBTut:
                    if (playerInput.lookAround)
                    {
                        DeactivateControlsTutorial(tutorialInProgress);
                        RMBTutFin = true;
                        tutorialInProgress = ControlsHint.Tutorial.None;
                    }
                    break;
                case ControlsHint.Tutorial.HonkTut:
                    if (Input.GetMouseButtonDown(0))
                    {
                        DeactivateControlsTutorial(tutorialInProgress);
                        honkTutFin = true;
                        //StartCoroutine(ActivateWithDelay(tutorialInProgress, ControlsHint.Tutorial.RMBTut));
                    }
                    break;
                case ControlsHint.Tutorial.SpaceTut:
                    if (playerInput.jump)
                    {
                        DeactivateControlsTutorial(tutorialInProgress);
                        spaceTutFin = true;
                        tutorialInProgress = ControlsHint.Tutorial.None;
                    }
                    break;
            }
        }
    }

    //Sets true to item collected
    public void CollectItem(Enum item)
    {
        switch (item)
        {
            case global::CollectItem.Item.Banana:
                hasBanana = true;
                //bananaIcon.SetActive(true);
                break;
            case global::CollectItem.Item.Fish:
                hasFish++;
                //fishIcon.SetActive(true);
                break;
        }
    }

    //Sets false to item collected
    public bool UseItem(Enum item)
    {
        switch (item)
        {
            case global::CollectItem.Item.Banana:
                if (hasBanana)
                {
                    hasBanana = false;
                    //bananaIcon.SetActive(false);
                    return true;
                }
                break;
                //Call banana usage cam script or whatever
            case global::CollectItem.Item.Fish:
                if (hasFish == 3)
                {
                    hasFish = 0;
                    //fishIcon.SetActive(false);
                    return true;
                }
                break;
            //Same thing as banana but for fish
        }
        return false;
    }

    //Activate the controls icon and sets the current tutorial to the parameter
    public void ActivateControlsTutorial(ControlsHint.Tutorial tutorial)
    {
        //tutCanvas.SetActive(true);

        switch (tutorial)
        {
            case ControlsHint.Tutorial.MoveTut:
                if (!moveTutFin)
                {
                    tutCanvas.SetActive(true);
                    moveTutIcon.SetActive(true);
                    //tutorialInProgress = tutorial;
                }
                break;
            case ControlsHint.Tutorial.RMBTut:
                if (!RMBTutFin)
                {
                    tutCanvas.SetActive(true);
                    RMBTutIcon.SetActive(true);
                    //tutorialInProgress = tutorial;
                }
                break;
            case ControlsHint.Tutorial.HonkTut:
                if (!honkTutFin)
                {
                    tutCanvas.SetActive(true);
                    honkTutIcon.SetActive(true);
                    //tutorialInProgress = tutorial;
                }
                break;
            case ControlsHint.Tutorial.SpaceTut:
                if (!spaceTutFin)
                {
                    tutCanvas.SetActive(true);
                    spaceTutIcon.SetActive(true);
                    //tutorialInProgress = tutorial;
                }
                break;
            default:
                Debug.Log("error in entering tutorial zone");
                break;
        }
        tutorialInProgress = tutorial;
    }
    
    //Deactivate the controls icon
    public void DeactivateControlsTutorial(ControlsHint.Tutorial Tutorial)
    {
        if (tutorialInProgress != Tutorial)
        {
            return;
        }
        tutCanvas.SetActive(false);
        switch (Tutorial)
        {
            case ControlsHint.Tutorial.MoveTut:
                //Dissolve tutorial icon
                moveTutIcon.SetActive(false);
                break;
            case ControlsHint.Tutorial.RMBTut:
                //Dissolve tutorial icon
                RMBTutIcon.SetActive(false);
                break;
            case ControlsHint.Tutorial.HonkTut:
                //Dissolve tutorial icon
                honkTutIcon.SetActive(false);
                break;
            case ControlsHint.Tutorial.SpaceTut:
                //Dissolve tutorial icon
                spaceTutIcon.SetActive(false);
                break;
            default:
                Debug.Log("error on leaving tutorial zone");
                break;
        }
        tutorialInProgress = ControlsHint.Tutorial.None; //Sets current tutorial to none
    }

    public void ActivateTutorial(ControlsHint.Tutorial tutorial)
    {
        //Debug.Log(tutorialInProgress);
        if (tutorialInProgress == ControlsHint.Tutorial.None)
        {
            ActivateControlsTutorial(tutorial);
        }
    }

    private IEnumerator ActivateWithDelay(ControlsHint.Tutorial nextTutorial)
    {
        //Set dissolve to 0.5 and 0.5 dissolve appear
        DeactivateControlsTutorial(tutorialInProgress);
        tutorialInProgress = nextTutorial;
        yield return new WaitForSeconds(1f);
        //Debug.Log(tutorialInProgress);
        ActivateControlsTutorial(nextTutorial);
    }
}
