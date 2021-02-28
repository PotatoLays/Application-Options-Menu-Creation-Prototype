using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Singleton that is created during preload, attached to the settings canvas
public class SettingsMenu : MonoBehaviour
{
    private static SettingsMenu _instance;
    public static SettingsMenu Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SettingsMenu>();
            }
            return _instance;
        }
    }

    // constants
    const string FONTSIZE_INDEX = "fontSizeIndex";
    const string FONTCOLOUR_INDEX = "fontColourIndex";
    const string VOLUME = "volume";
    const string ENABLED_DARKMODE = "enabledDarkMode";

    public AudioMixer audioMixer;
    public TMP_Dropdown dropdownFontSize;
    public TMP_Dropdown dropdownFontColour;
    public Slider sliderVolume;
    public Toggle toggleDarkMode;

    public delegate void NotifySettingsMenuAction(); //delegate
    public static event NotifySettingsMenuAction OnCloseSettingsMenu; // event of closing settings menu
    public static event NotifySettingsMenuAction OnChangeSettingsMenu; // event of changing setting parameter

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.GetComponent<Canvas>().enabled = false;
        LoadSettings();
        SceneManager.LoadScene("Menu");
    }

    void Update()
    {
        // Press escape in Game Scene
        if (SceneManager.GetActiveScene().name == "Game" && Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.GetComponent<Canvas>().enabled = true;
        }
    }

    // Load the previously stored settings according to what is in PlayerPrefs, initialize the UI correctly with these values.
    public void LoadSettings()
    {
        // Load font size setting, index 1 (medium) is default
        ChangeGameText.fontSizeIndex = PlayerPrefs.GetInt(FONTSIZE_INDEX, 1);
        InitializeDropdownFontSize();
        // Load font colour setting, index 0 (default [black/white]) is default
        ChangeGameText.fontColourIndex = PlayerPrefs.GetInt(FONTCOLOUR_INDEX, 0);
        InitializeDropdownFontColour();
        // Load volume setting, at 1 it is max volume
        ChangeGameText.volume = PlayerPrefs.GetFloat(VOLUME, 1);
        InitializeVolumeSlider();
        // Load dark mode setting, default is false (0)
        ChangeGameText.enabledDarkMode = PlayerPrefs.GetInt(ENABLED_DARKMODE, 0) == 1;
        InitializeDarkModeToggle();
    }

    void InitializeDropdownFontSize()
    {
        dropdownFontSize.value = ChangeGameText.fontSizeIndex;
    }
    void InitializeDropdownFontColour()
    {
        dropdownFontColour.value = ChangeGameText.fontColourIndex;
    }

    void InitializeVolumeSlider()
    {
        sliderVolume.value = ChangeGameText.volume;
    }

    void InitializeDarkModeToggle()
    {
        toggleDarkMode.isOn = ChangeGameText.enabledDarkMode;
    }

    public void CallOnChangeSettingsMenu()
    {
        // Set the event that the settings has been changed.
        if (OnChangeSettingsMenu != null)
        {
            OnChangeSettingsMenu();
        }
    }

    public void CallOnClosedSettingsMenu()
    {
        // Set the event that the settings menu has been closed.
        if (OnCloseSettingsMenu != null)
        {
            OnCloseSettingsMenu();
        }
    }

    public void DropdownFontSizeChanged()
    {
        ChangeGameText.fontSizeIndex = dropdownFontSize.value;
    }

    public void DropdownFontColourChanged()
    {
        ChangeGameText.fontColourIndex = dropdownFontColour.value;
    }

    public void SetVolume(float volume)
    {
        // "volume" is the name of exposed parameter in the Audio Mixer.
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        ChangeGameText.volume = volume;
    }

    public void SetDarkMode(bool isDark)
    {
        ChangeGameText.enabledDarkMode = isDark;
    }

    public void ReturnToMainMenu()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
        SceneManager.LoadScene("Menu");
    }

    public void PressBack()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void SavePlayerPrefs()
    {
        // fontSizeIndex
        PlayerPrefs.SetInt(FONTSIZE_INDEX, ChangeGameText.fontSizeIndex);
        // fontColourIndex
        PlayerPrefs.SetInt(FONTCOLOUR_INDEX, ChangeGameText.fontColourIndex);
        // volume
        PlayerPrefs.SetFloat(VOLUME, ChangeGameText.volume);
        // enabledDarkMode
        int intBool = ChangeGameText.enabledDarkMode? 1 : 0;
        PlayerPrefs.SetInt(ENABLED_DARKMODE, intBool);
    }
}
