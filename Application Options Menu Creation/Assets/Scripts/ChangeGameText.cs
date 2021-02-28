using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeGameText : MonoBehaviour
{
    // Constants
    Color BLACK = new Color32(0, 0, 0, 255);
    Color WHITE = new Color32(255, 255, 255, 255);
    Color BLUE = new Color32(37, 111, 212, 255);

    int[] fontSizes = new int[] { 12, 16, 20 };

    public static int fontSizeIndex;
    public static int fontColourIndex;
    public static float volume;
    public static bool enabledDarkMode;

    public TextMeshProUGUI TMP_gameText;
    public Image gameTextPanel;

    void Awake()
    {
        LoadSetting();
        SettingsMenu.OnChangeSettingsMenu += LoadSetting;
    }

    void OnDestroy()
    {
        SettingsMenu.OnChangeSettingsMenu -= LoadSetting;
    }

    void LoadSetting()
    {
        // load FontSize
        TMP_gameText.fontSize = fontSizes[fontSizeIndex];
        // load light/dark mode
        gameTextPanel.color = enabledDarkMode ? BLACK : WHITE;
        // load FontColour
        LoadFontColour();
    }

    void LoadFontColour()
    {
        switch (fontColourIndex)
        {
            // default : black or white depending on darkMode
            case 0:
                TMP_gameText.color = enabledDarkMode ? WHITE : BLACK;
                break;
            // blue
            case 1:
                TMP_gameText.color = BLUE;
                break;
            default:
                Debug.LogError("fontColourIndex has illegal value.");
                break;
        }
    }
}
