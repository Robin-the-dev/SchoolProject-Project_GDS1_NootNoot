using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    public static Dictionary<string, object> puzzlesComp;

    private void Awake()
    {
        puzzlesComp = new Dictionary<string, object>();
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        puzzlesComp.Add("monkey", monkeyPuzzleFinished);
        puzzlesComp.Add("lion", lionPuzzleFinished);
        puzzlesComp.Add("fish", fishPuzzleFinished);
        puzzlesComp.Add("parrot", parrotPuzzleFinished);
    }
    #endregion

    private bool monkeyPuzzleFinished, lionPuzzleFinished, fishPuzzleFinished, parrotPuzzleFinished;
    [SerializeField] private GameObject door;

    [SerializeField] private PenguinsManager penguinsManager;

    [SerializeField] private PlayableDirector playableDirector;

    [SerializeField] private PlayerMovement playerMovement;
    
    //This is a terrible implementation but oh wells
    [SerializeField] private ChangeEyes[] animalHeads;
    [SerializeField] private ChangeEyesLion lion;

    private void Update()
    {
        if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.O))
        {
            EndCutscene();
            AnalyticsEvent.Custom("Forced door open");
        }
    }

    public void MonkeyPuzzle()
    {
        monkeyPuzzleFinished = true;
        puzzlesComp["monkey"] = monkeyPuzzleFinished;
        AnalyticsEvent.Custom("Monkey Puzzle Completed");
        //Call camera scripts
        //Disable movement
        disableMovementAndPlayCamera();
        
        //Makes monkey eyes glow
        animalHeads[0].Activate();
        
        /*if (lionPuzzleFinished && parrotPuzzleFinished)
        {
           EndCutscene();
           //AnalyticsEvent.LevelComplete("L1", 0, puzzlesComp);
        }*/
         CheckFinished();
    }

    public void LionPuzzle()
    {
        lionPuzzleFinished = true;
        puzzlesComp["lion"] = lionPuzzleFinished;
        AnalyticsEvent.Custom("Lion Puzzle Completed");
        //Call camera scripts
        //Disable movement
        disableMovementAndPlayCamera();
        
        //Makes eyes glow
        lion.Activate();
        
        /*if (monkeyPuzzleFinished && parrotPuzzleFinished)
        {
            EndCutscene();
            //AnalyticsEvent.LevelComplete("L1", 0, puzzlesComp);
        }*/
        CheckFinished();
    }

    public void FishPuzzle()
    {
        fishPuzzleFinished = true;
        puzzlesComp["fish"] = fishPuzzleFinished;
        penguinsManager.FishPuzzleComplete();
        AnalyticsEvent.Custom("Gave fish");
    }

    public void ParrotPuzzle()
    {
        parrotPuzzleFinished = true;
        puzzlesComp["parrot"] = parrotPuzzleFinished;
        
        //Makes parrot eyes glow
        animalHeads[1].Activate();
        
        //Call camera scripts
        //Disable movement
        disableMovementAndPlayCamera();
        
        
        AnalyticsEvent.Custom("Parrot puzzle completed");
        //Debug.Log("parrot puzzle fin");
        CheckFinished();
    }

    private void CheckFinished()
    {
        if (lionPuzzleFinished && monkeyPuzzleFinished && parrotPuzzleFinished)
        {
            EndCutscene();
        }
    }

    private void EndCutscene()
    {
        door.GetComponent<Animator>().SetBool("isOpen", true);
        /*Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;*/
    }
    
    public bool GetLionPuzzle(){
      return lionPuzzleFinished;
    }

    public void LevelComplete()
    {
        AnalyticsEvent.LevelComplete("L1", 0, puzzlesComp);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);//Make it next level when we go next time
    }

    private void disableMovementAndPlayCamera() {
        playableDirector.Play();
        StartCoroutine(enumerator());
    }

    private IEnumerator enumerator() {
        playerMovement.enabled = false;
        yield return new WaitForSeconds(1f);
        playerMovement.enabled = true;
    }
}
