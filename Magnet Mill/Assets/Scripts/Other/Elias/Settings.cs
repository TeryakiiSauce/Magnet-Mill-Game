using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class Settings : MonoBehaviour
{
    public GameObject settingsCanvas;
    public TextMeshProUGUI qualitySelectedLabel, renderDistSelectedLabel, mouseParallaxSelectedLabel;

    private GameObject[] virtualCamerasGO;

    // Default values
    private int defaultQuality, defaultRenderDistance = 110, defaultMouseParallax = 4;

    // Game objects & their slider component
    private GameObject qualitySliderGO; private Slider qualitySlider;
    private GameObject maxRenderDistSliderGO; private Slider maxRenderDistSlider;
    private GameObject mouseParallaxSliderGO; private Slider mouseParallaxSlider;

    // private void Awake()
    // {
    //     settingsCanvas = new GameObject();

    //     Debugging(true);

    //     if (SceneManager.GetActiveScene().name == "Main Menu")
    //     {
    //         if (!PlayerPrefs.HasKey("isFirstRun"))
    //         {
    //             InitializeUserSettings();
    //         }
    //     }
    //     else
    //     {
    //         LoadLevelSettings();
    //     }
    // }

    // Start is called before the first frame update
    void Start()
    {
        settingsCanvas = new GameObject();

        Debugging(false);

        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            if (!PlayerPrefs.HasKey("isFirstRun"))
            {
                InitializeUserSettings();
            }
        }
        else
        {
            LoadLevelSettings();
        }
    }

    private void InitializeUserSettings()
    {
        defaultQuality = QualitySettings.GetQualityLevel();
        PlayerPrefs.SetInt("qualitySetting", defaultQuality);
        PlayerPrefs.SetInt("maxRenderDistance", defaultRenderDistance);
        PlayerPrefs.SetInt("mouseParallax", defaultMouseParallax * 5);

        PlayerPrefs.SetString("isFirstRun", "false");
    }

    // When button is pressed from the main menu or pause menu
    public void SettingsButtonClicked()
    {
        settingsCanvas.gameObject.SetActive(true);

        qualitySliderGO = GameObject.FindGameObjectWithTag("QualitySettingSlider");
        qualitySlider = qualitySliderGO.GetComponent<Slider>();


        maxRenderDistSliderGO = GameObject.FindGameObjectWithTag("MaxRenderDistanceSetting");
        maxRenderDistSlider = maxRenderDistSliderGO.GetComponent<Slider>();

        mouseParallaxSliderGO = GameObject.FindGameObjectWithTag("MouseParallaxSetting");
        mouseParallaxSlider = mouseParallaxSliderGO.GetComponent<Slider>();

        LoadSettings();
    }

    public void CloseButtonClicked()
    {
        GoBack();
    }

    public void SaveButtonClicked()
    {
        SaveSettings();
        GoBack();
    }

    public void ResetToDefaultsButtonClicked()
    {
        ResetSettings();
        GoBack();
    }

    // Loads settings for the current level
    private void LoadLevelSettings()
    {
        virtualCamerasGO = GameObject.FindGameObjectsWithTag("Virtual Camera");

        foreach (var virtualCam in virtualCamerasGO)
        {
            virtualCam.GetComponent<CinemachineVirtualCamera>().m_Lens.FarClipPlane = PlayerPrefs.GetInt("maxRenderDistance");
        }
    }

    // Loads settings for user to change when paused or in main menu
    // Checks if user settings preferences keys exist and uses them; otherwise, it creates new keys
    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey("qualitySetting"))
        {
            qualitySlider.value = PlayerPrefs.GetInt("qualitySetting");
            QualitySettings.SetQualityLevel(((int)qualitySlider.value));
        }
        else
        {
            qualitySlider.value = QualitySettings.GetQualityLevel();
            PlayerPrefs.SetInt("qualitySetting", ((int)qualitySlider.value));
        }
        RefreshLabel("quality");


        if (PlayerPrefs.HasKey("maxRenderDistance"))
        {
            maxRenderDistSlider.value = PlayerPrefs.GetInt("maxRenderDistance");
        }
        else
        {
            maxRenderDistSlider.value = defaultRenderDistance;
            PlayerPrefs.SetInt("maxRenderDistance", ((int)maxRenderDistSlider.value));
        }
        RefreshLabel("render distance");


        if (PlayerPrefs.HasKey("mouseParallax"))
        {
            mouseParallaxSlider.value = PlayerPrefs.GetInt("mouseParallax") / 5;
        }
        else
        {
            mouseParallaxSlider.value = defaultMouseParallax;
            PlayerPrefs.SetInt("mouseParallax", ((int)mouseParallaxSlider.value) * 5);
        }
        RefreshLabel("mouse parallax");


    }

    private void SaveSettings()
    {
        QualitySettings.SetQualityLevel(((int)qualitySlider.value));
        PlayerPrefs.SetInt("qualitySetting", ((int)qualitySlider.value));

        PlayerPrefs.SetInt("maxRenderDistance", ((int)maxRenderDistSlider.value));

        PlayerPrefs.SetInt("mouseParallax", ((int)mouseParallaxSlider.value) * 5);
    }

    private void ResetSettings()
    {
        PlayerPrefs.DeleteKey("isFirstRun");
        PlayerPrefs.DeleteKey("qualitySetting");
        PlayerPrefs.DeleteKey("maxRenderDistance");
        PlayerPrefs.DeleteKey("mouseParallax");

        InitializeUserSettings();
    }

    private void GoBack()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RefreshLabel(string forWhat)
    {
        switch (forWhat)
        {
            case "quality":
                qualitySelectedLabel.text = QualitySettings.names[((int)qualitySlider.value)];
                break;

            case "render distance":
                renderDistSelectedLabel.text = maxRenderDistSlider.value.ToString() + " meters";
                break;

            case "mouse parallax":
                int tempMouseParallaxSetting = ((int)mouseParallaxSlider.value);
                string tempStr = "";
                mouseParallaxSelectedLabel.transform.rotation = Quaternion.Euler(0, 0, 0);

                switch (tempMouseParallaxSetting)
                {
                    case 0:
                        tempStr = "Off";
                        break;
                    case 1:
                        tempStr = "Low";
                        break;
                    case 2:
                        tempStr = "Medium";
                        break;
                    case 3:
                        tempStr = "High";
                        break;
                    case 4:
                        tempStr = "Shake that mouse!";
                        mouseParallaxSelectedLabel.transform.Rotate(0, 0, 4, Space.World);
                        break;
                    default:
                        Debug.LogError("Something went wrong in the script [Settings.cs]... -> Go to RefreshLabel() -> Mouse Parallax");
                        break;
                }

                mouseParallaxSelectedLabel.text = tempStr;
                break;

            default:
                Debug.LogError("Something went wrong in the script [Settings.cs]... -> Go to RefreshLabel()");
                break;
        }
    }

    private void Debugging(bool isActive)
    {
        if (isActive)
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            Debug.Log($"First time?: {PlayerPrefs.GetString("isFirstRun")}");
            Debug.Log($"Quality: {QualitySettings.names[PlayerPrefs.GetInt("qualitySetting")]}");
            Debug.Log($"Max Render Distance: {PlayerPrefs.GetInt("maxRenderDistance")} meters");

            int tempMouseParallaxSetting = PlayerPrefs.GetInt("mouseParallax") / 5;
            string tempStr = "";

            switch (tempMouseParallaxSetting)
            {
                case 0:
                    tempStr = "Off";
                    break;
                case 1:
                    tempStr = "Low";
                    break;
                case 2:
                    tempStr = "Medium";
                    break;
                case 3:
                    tempStr = "High";
                    break;
                case 4:
                    tempStr = "Shake that mouse!";
                    break;
                default:
                    Debug.LogError("Something went wrong in the script [Settings.cs]... -> Go to Debugging()");
                    break;
            }

            Debug.Log($"Mouse Parallax: {tempStr}");
        }
    }
}
