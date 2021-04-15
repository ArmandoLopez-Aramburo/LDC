using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("Menu's")]
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject ExitMenu;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Settings(bool state)
    {
        MenuSelect(SettingsMenu, state);
    }

    public void ExitGame(bool state)
    {
        MenuSelect(ExitMenu, state);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MenuSelect(GameObject panel, bool state)
    {
        if (state) panel.SetActive(true);
        else panel.SetActive(false);
    }
}
