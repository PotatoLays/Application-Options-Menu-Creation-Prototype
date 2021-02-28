using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        // register with an event (when Settings Menu closes)
        SettingsMenu.OnCloseSettingsMenu += settingsMenu_OnCloseSettingsMenu; 
    }

    // event handler: when settings menu is closed, open main menu
    void settingsMenu_OnCloseSettingsMenu()
    {
        gameObject.SetActive(true);
    }

    public void PlayGame()
    {
        // Unsubscribe from listening to Settings Menu when transitioning to Game Scene
        SettingsMenu.OnCloseSettingsMenu -= settingsMenu_OnCloseSettingsMenu; 
        SceneManager.LoadScene("Game");
    }

    public void OpenSettingsMenu()
    {
        gameObject.SetActive(false);
        GameObject settingsCanvas = GameObject.Find("SettingsCanvas");
        settingsCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
