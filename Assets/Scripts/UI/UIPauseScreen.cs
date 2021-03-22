using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UIPauseScreen : MonoBehaviour
{
    public void OpenMenu(GameObject otherMenu)
    {
        otherMenu.SetActive(true);
        //Call tween
    }

    public void CloseMenu(GameObject currentMenu)
    {
        currentMenu.SetActive(false);
        //
    }
    
    public void GoToMenu()
    {
        UnpauseInMenu();
        SceneManager.LoadScene(0);
    }

    public void ResetPlayer()
    {
        UnpauseInMenu();
        CheckpointManager.Instance.teleportToCurrCp();
        /*Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);*/
    }

    private void UnpauseInMenu()
    {
        Time.timeScale = 1;
        AudioManager.Instance.RaiseMaster();
        AudioManager.Instance.ResetSnapshot();
    }

}
