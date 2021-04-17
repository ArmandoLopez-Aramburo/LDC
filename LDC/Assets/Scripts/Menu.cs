using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("Menu's")]
    [SerializeField] public GameObject MainMenu;
    [SerializeField] public GameObject SettingsMenu;
    [SerializeField] public GameObject CreditsMenu;
    [SerializeField] public GameObject ExitMenu;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Settings(bool state)
    {
        MenuSelect(SettingsMenu, state);
    }

    public void Credits(bool state)
    {
        MenuSelect(CreditsMenu, state);
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
