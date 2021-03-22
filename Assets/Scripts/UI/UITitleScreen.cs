using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITitleScreen : MonoBehaviour
{
    [SerializeField] private AudioClip honk;
    [SerializeField] private AudioSource audioSource;
    public void OpenMenu(GameObject otherMenu)
    {
        otherMenu.SetActive(true);
        
    }

    public void CloseMenu(GameObject currentMenu)
    {
        currentMenu.SetActive(false);
    }

    public void GoToLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void PlayHonk()
    {
        audioSource.PlayOneShot(honk);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
