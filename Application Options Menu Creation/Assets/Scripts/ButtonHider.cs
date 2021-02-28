using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHider : MonoBehaviour
{
    [SerializeField] string hideWithScene;

    void Awake()
    {
        // Listen to when the active scene changes and then update hidebutton status
        SceneManager.activeSceneChanged += HideButton;
    }

    void HideButton(Scene s1, Scene s2)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == hideWithScene)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}