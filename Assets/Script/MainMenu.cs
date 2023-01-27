using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject settingsWindowPanel;
    public AudioSource back;
    public void StartGame()
    {
        Debug.Log("coucou");
        back.Stop();
        SceneManager.LoadScene("Level 1");
    }

    public void SettingsButton()
    {
        settingsWindowPanel.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingsWindowPanel.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
