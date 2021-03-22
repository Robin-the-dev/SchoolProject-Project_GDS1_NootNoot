using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseManager : MonoBehaviour
{
    public static bool paused;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject pauseMenu, settingsMenu, controlsMenu;

    public delegate void GamePaused();

    public static GamePaused gamePaused;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if (paused)
            {
                OpenPauseMenu();
                gamePaused?.Invoke();
            }
            else
            {
                ClosePauseMenu();
            }
            
        }
    }

    public void OpenPauseMenu()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        //Lowers all audio levels by a bit
        AudioManager.Instance.LowerMaster();
        
        //Disable player controls
        pauseMenuCanvas.SetActive(true);
        //Debug.Log("Open");
    }
    
    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        pauseMenuCanvas.SetActive(false);
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        //Raises all audio to normal
        AudioManager.Instance.RaiseMaster();
        if (!pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
            settingsMenu.SetActive(false);
            controlsMenu.SetActive(false);
        }
        //Debug.Log("close");
    }
}
