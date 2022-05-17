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
    public TextMeshProUGUI qualitySelectedLabel, renderDistSelectedLabel;

    private int defaultRenderDistance = 110, defaultQuality;
    private GameObject qualitySliderGameObject; private Slider qualitySlider;
    private GameObject maxRenderDistSliderGameObject; private Slider maxRenderDistSlider;
    private GameObject[] virtualCamerasGO;

    // Start is called before the first frame update
    void Start()
    {
        settingsCanvas = new GameObject();

        Debugging(true);

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
        //if () { PlayerPrefs.SetString("isFirstRun", "false"); return; }

        defaultQuality = QualitySettings.GetQualityLevel();
        PlayerPrefs.SetInt("qualitySetting", defaultQuality);
        PlayerPrefs.SetInt("maxRenderDistance", defaultRenderDistance);

        PlayerPrefs.SetString("isFirstRun", "false");
    }

    public void SettingsButtonClicked()
    {
        settingsCanvas.gameObject.SetActive(true);

        qualitySliderGameObject = GameObject.FindGameObjectWithTag("QualitySettingSlider");
        qualitySlider = qualitySliderGameObject.GetComponent<Slider>();


        maxRenderDistSliderGameObject = GameObject.FindGameObjectWithTag("MaxRenderDistanceSetting");
        maxRenderDistSlider = maxRenderDistSliderGameObject.GetComponent<Slider>();

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

    }

    private void SaveSettings()
    {
        QualitySettings.SetQualityLevel(((int)qualitySlider.value));
        PlayerPrefs.SetInt("qualitySetting", ((int)qualitySlider.value));

        PlayerPrefs.SetInt("maxRenderDistance", ((int)maxRenderDistSlider.value));
    }

    private void ResetSettings()
    {
        PlayerPrefs.DeleteKey("isFirstRun");
        PlayerPrefs.DeleteKey("qualitySetting");
        PlayerPrefs.DeleteKey("maxRenderDistance");

        InitializeUserSettings();
    }

    private void GoBack()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Debugging(bool isActive)
    {
        if (isActive)
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            Debug.Log($"First time?: {PlayerPrefs.GetString("isFirstRun")}");
            Debug.Log($"Quality: {QualitySettings.names[PlayerPrefs.GetInt("qualitySetting")]}");
            Debug.Log($"Max Render Distance: {PlayerPrefs.GetInt("maxRenderDistance")} meters");
        }
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
            default:
                break;
        }
    }
}
