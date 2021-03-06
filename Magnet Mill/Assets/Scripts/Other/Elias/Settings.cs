using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class Settings : MonoBehaviour
{
    public GameObject settingsCanvas;
    public TextMeshProUGUI qualitySelectedLabel, renderDistSelectedLabel, mouseParallaxSelectedLabel, volumeSelectedLabel;
    public TMP_InputField refreshRateEnteredLabel;
    public Toggle aOToggle, bloomToggle, cGToggle, chrAToggle, dOFToggle, grainToggle, lensDistToggle, motBlurToggle, sSRTogle, vinToggle, fPSToggle, audioSyncToggle;

    // Default values
    private int defaultQuality, defaultRenderDistance = 110, defaultMouseParallax = 4; private bool defaultToggleOn = true, defaultToggleOff = false; private float defaultAudioVol = 100.0f, defaultFPSRefreshRate = 2.0f;

    // --- --- --- ---

    // Game objects & their slider component [used in settings menu]
    private GameObject qualitySliderGO; private Slider qualitySlider;
    private GameObject maxRenderDistSliderGO; private Slider maxRenderDistSlider;
    private GameObject mouseParallaxSliderGO; private Slider mouseParallaxSlider;
    private GameObject audioVolSliderGO; private Slider audioVolSlider;
    private float refreshRateSec;


    // Game objects for current level [used to get required current level game objects] 
    //public TextMeshProUGUI fPSCounterLabel;
    private GameObject[] virtualCamerasGO;
    private GameObject postProcessingGO; private PostProcessVolume ppVolume;

    // --- --- --- ---

    // ----------------------------------\\
    // --- POST PROCESSING VARIABLES --- \\
    // ----------------------------------\\

    private AmbientOcclusion _ao;
    private Bloom _bloom;
    private ChromaticAberration _chrA;
    private Grain _grain;
    private Vignette _vin;
    private ScreenSpaceReflections _ssr;
    private MotionBlur _motBlur;
    private LensDistortion _lensDist;
    private DepthOfField _dof;
    private ColorGrading _cg;

    // --------------------------------------\\
    // --- END POST PROCESSING VARIABLES --- \\
    // --------------------------------------\\

    // --- --- --- ---

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

    // --- --- --- ---

    // ----------------------\\
    // --- BUTTON EVENTS --- \\
    // ----------------------\\

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

        audioVolSliderGO = GameObject.FindGameObjectWithTag("VolumeSetting");
        audioVolSlider = audioVolSliderGO.GetComponent<Slider>();

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

    public void SelectAllPostProcessing()
    {
        aOToggle.isOn = true;
        bloomToggle.isOn = true;
        cGToggle.isOn = true;
        chrAToggle.isOn = true;
        dOFToggle.isOn = true;
        grainToggle.isOn = true;
        lensDistToggle.isOn = true;
        motBlurToggle.isOn = true;
        sSRTogle.isOn = true;
        vinToggle.isOn = true;
    }

    public void DeselectAllPostProcessing()
    {
        aOToggle.isOn = false;
        bloomToggle.isOn = false;
        cGToggle.isOn = false;
        chrAToggle.isOn = false;
        dOFToggle.isOn = false;
        grainToggle.isOn = false;
        lensDistToggle.isOn = false;
        motBlurToggle.isOn = false;
        sSRTogle.isOn = false;
        vinToggle.isOn = false;
    }

    public void RefreshRateValueChanged()
    {
        //print("(unfiltered) entered: " + refreshRateEnteredLabel.text);

        if (refreshRateEnteredLabel.text.Contains("-"))
        {
            refreshRateEnteredLabel.text = "Negative values not allowed!";
        }
        else if (refreshRateEnteredLabel.text == "" || refreshRateEnteredLabel.text == null)
        {
            refreshRateEnteredLabel.text = defaultFPSRefreshRate.ToString();
        }

        float tempRefreshRateEntered = float.Parse(refreshRateEnteredLabel.text);

        if (tempRefreshRateEntered < 0.1f)
        {
            refreshRateEnteredLabel.text = "0.1";
        }
        else if (tempRefreshRateEntered > 10f)
        {
            refreshRateEnteredLabel.text = "10";
        }
        else
        {
            refreshRateSec = float.Parse(refreshRateEnteredLabel.text);
        }

        //print("(filtered) entered: " + refreshRateSec);
    }

    // --------------------------\\
    // --- END BUTTON EVENTS --- \\
    // --------------------------\\

    // --- --- --- ---

    // To set the default values during first run or when resetting to defaults
    private void InitializeUserSettings()
    {
        defaultQuality = QualitySettings.GetQualityLevel();
        PlayerPrefs.SetInt("qualitySetting", defaultQuality);
        PlayerPrefs.SetInt("maxRenderDistance", defaultRenderDistance);
        PlayerPrefs.SetInt("mouseParallax", defaultMouseParallax * 5);
        PlayerPrefs.SetFloat("audioVolume", defaultAudioVol);

        PlayerPrefs.SetInt("fpsToggle", (defaultToggleOn ? 1 : 0));
        PlayerPrefs.SetFloat("fpsRefreshRate", defaultFPSRefreshRate);
        refreshRateSec = defaultFPSRefreshRate;
        PlayerPrefs.SetInt("audioSyncToggle", (defaultToggleOff ? 1 : 0));

        PlayerPrefs.SetInt("aoToggle", (defaultToggleOn ? 1 : 0));
        PlayerPrefs.SetInt("bloomToggle", (defaultToggleOn ? 1 : 0));
        PlayerPrefs.SetInt("cgToggle", (defaultToggleOn ? 1 : 0));
        PlayerPrefs.SetInt("chrAToggle", (defaultToggleOn ? 1 : 0));
        PlayerPrefs.SetInt("dofToggle", (defaultToggleOn ? 1 : 0));
        PlayerPrefs.SetInt("grainToggle", (defaultToggleOn ? 1 : 0));
        PlayerPrefs.SetInt("lensDistToggle", (defaultToggleOn ? 1 : 0));
        PlayerPrefs.SetInt("motBlurToggle", (defaultToggleOn ? 1 : 0));
        PlayerPrefs.SetInt("ssrToggle", (defaultToggleOn ? 1 : 0));
        PlayerPrefs.SetInt("vinToggle", (defaultToggleOn ? 1 : 0));

        PlayerPrefs.SetString("isFirstRun", "false");
    }

    // Loads settings for the current level
    private void LoadLevelSettings()
    {
        virtualCamerasGO = GameObject.FindGameObjectsWithTag("Virtual Camera");

        if (GameController.instance.currentLevel == "Level4")
        {
            foreach (var virtualCam in virtualCamerasGO)
            {
                virtualCam.GetComponent<CinemachineVirtualCamera>().m_Lens.FarClipPlane = defaultRenderDistance;
            }
        }
        else
        {
            foreach (var virtualCam in virtualCamerasGO)
            {
                virtualCam.GetComponent<CinemachineVirtualCamera>().m_Lens.FarClipPlane = PlayerPrefs.GetInt("maxRenderDistance");
            }
        }

        postProcessingGO = GameObject.FindGameObjectWithTag("Post Processing");
        ppVolume = postProcessingGO.GetComponent<PostProcessVolume>();

        ppVolume.profile.TryGetSettings(out _ao);
        ppVolume.profile.TryGetSettings(out _bloom);
        ppVolume.profile.TryGetSettings(out _cg);
        ppVolume.profile.TryGetSettings(out _chrA);
        ppVolume.profile.TryGetSettings(out _dof);
        ppVolume.profile.TryGetSettings(out _grain);
        ppVolume.profile.TryGetSettings(out _lensDist);
        ppVolume.profile.TryGetSettings(out _motBlur);
        ppVolume.profile.TryGetSettings(out _ssr);
        ppVolume.profile.TryGetSettings(out _vin);

        // Converts the int value passed to boolean to be able to activate or deactivate the toggles
        _ao.active = PlayerPrefs.GetInt("aoToggle") != 0;
        _bloom.active = PlayerPrefs.GetInt("bloomToggle") != 0;
        _cg.active = PlayerPrefs.GetInt("cgToggle") != 0;
        _chrA.active = PlayerPrefs.GetInt("chrAToggle") != 0;
        _dof.active = PlayerPrefs.GetInt("dofToggle") != 0;
        _grain.active = PlayerPrefs.GetInt("grainToggle") != 0;
        _lensDist.active = PlayerPrefs.GetInt("lensDistToggle") != 0;
        _motBlur.active = PlayerPrefs.GetInt("motBlurToggle") != 0;
        _ssr.active = PlayerPrefs.GetInt("ssrToggle") != 0;
        _vin.active = PlayerPrefs.GetInt("vinToggle") != 0;
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

        if (PlayerPrefs.HasKey("audioVolume"))
        {
            audioVolSlider.value = PlayerPrefs.GetFloat("audioVolume");
        }
        else
        {
            audioVolSlider.value = defaultAudioVol;
            PlayerPrefs.SetFloat("audioVolume", defaultAudioVol);
        }
        RefreshLabel("volume");

        if (PlayerPrefs.HasKey("fpsToggle"))
        {
            fPSToggle.isOn = PlayerPrefs.GetInt("fpsToggle") != 0;
        }
        else
        {
            fPSToggle.isOn = defaultToggleOn;
            PlayerPrefs.SetInt("fpsToggle", (defaultToggleOn ? 1 : 0));
        }


        if (PlayerPrefs.HasKey("fpsRefreshRate"))
        {
            refreshRateEnteredLabel.text = PlayerPrefs.GetFloat("fpsRefreshRate").ToString();
        }
        else
        {
            refreshRateEnteredLabel.text = defaultFPSRefreshRate.ToString();
            PlayerPrefs.SetFloat("fpsRefreshRate", defaultFPSRefreshRate);
        }
        refreshRateSec = PlayerPrefs.GetFloat("fpsRefreshRate");


        if (PlayerPrefs.HasKey("audioSyncToggle"))
        {
            audioSyncToggle.isOn = PlayerPrefs.GetInt("audioSyncToggle") != 0;
        }
        else
        {
            audioSyncToggle.isOn = defaultToggleOff;
            PlayerPrefs.SetInt("audioSyncToggle", (defaultToggleOff ? 1 : 0));
        }


        // POST PROCESSING SECTION - using "IF TRUE" only for organization purposes; to collapse code.

        if (true)
        {
            if (PlayerPrefs.HasKey("aoToggle"))
            {
                aOToggle.isOn = PlayerPrefs.GetInt("aoToggle") != 0;
            }
            else
            {
                aOToggle.isOn = defaultToggleOn;
                PlayerPrefs.SetInt("aoToggle", (defaultToggleOn ? 1 : 0));
            }

            if (PlayerPrefs.HasKey("bloomToggle"))
            {
                bloomToggle.isOn = PlayerPrefs.GetInt("bloomToggle") != 0;
            }
            else
            {
                bloomToggle.isOn = defaultToggleOn;
                PlayerPrefs.SetInt("bloomToggle", (defaultToggleOn ? 1 : 0));
            }

            if (PlayerPrefs.HasKey("cgToggle"))
            {
                cGToggle.isOn = PlayerPrefs.GetInt("cgToggle") != 0;
            }
            else
            {
                cGToggle.isOn = defaultToggleOn;
                PlayerPrefs.SetInt("cgToggle", (defaultToggleOn ? 1 : 0));
            }

            if (PlayerPrefs.HasKey("chrAToggle"))
            {
                chrAToggle.isOn = PlayerPrefs.GetInt("chrAToggle") != 0;
            }
            else
            {
                chrAToggle.isOn = defaultToggleOn;
                PlayerPrefs.SetInt("chrAToggle", (defaultToggleOn ? 1 : 0));
            }

            if (PlayerPrefs.HasKey("dofToggle"))
            {
                dOFToggle.isOn = PlayerPrefs.GetInt("dofToggle") != 0;
            }
            else
            {
                dOFToggle.isOn = defaultToggleOn;
                PlayerPrefs.SetInt("dofToggle", (defaultToggleOn ? 1 : 0));
            }

            if (PlayerPrefs.HasKey("grainToggle"))
            {
                grainToggle.isOn = PlayerPrefs.GetInt("grainToggle") != 0;
            }
            else
            {
                grainToggle.isOn = defaultToggleOn;
                PlayerPrefs.SetInt("grainToggle", (defaultToggleOn ? 1 : 0));
            }

            if (PlayerPrefs.HasKey("lensDistToggle"))
            {
                lensDistToggle.isOn = PlayerPrefs.GetInt("lensDistToggle") != 0;
            }
            else
            {
                lensDistToggle.isOn = defaultToggleOn;
                PlayerPrefs.SetInt("lensDistToggle", (defaultToggleOn ? 1 : 0));
            }

            if (PlayerPrefs.HasKey("motBlurToggle"))
            {
                motBlurToggle.isOn = PlayerPrefs.GetInt("motBlurToggle") != 0;
            }
            else
            {
                motBlurToggle.isOn = defaultToggleOn;
                PlayerPrefs.SetInt("motBlurToggle", (defaultToggleOn ? 1 : 0));
            }

            if (PlayerPrefs.HasKey("ssrToggle"))
            {
                sSRTogle.isOn = PlayerPrefs.GetInt("ssrToggle") != 0;
            }
            else
            {
                sSRTogle.isOn = defaultToggleOn;
                PlayerPrefs.SetInt("ssrToggle", (defaultToggleOn ? 1 : 0));
            }

            if (PlayerPrefs.HasKey("vinToggle"))
            {
                vinToggle.isOn = PlayerPrefs.GetInt("vinToggle") != 0;
            }
            else
            {
                vinToggle.isOn = defaultToggleOn;
                PlayerPrefs.SetInt("vinToggle", (defaultToggleOn ? 1 : 0));
            }
        }

        // END OF POST PROCESSING
    }

    private void SaveSettings()
    {
        QualitySettings.SetQualityLevel(((int)qualitySlider.value));
        PlayerPrefs.SetInt("qualitySetting", ((int)qualitySlider.value));

        PlayerPrefs.SetInt("maxRenderDistance", ((int)maxRenderDistSlider.value));

        PlayerPrefs.SetInt("mouseParallax", ((int)mouseParallaxSlider.value) * 5);

        PlayerPrefs.SetFloat("audioVolume", audioVolSlider.value);

        PlayerPrefs.SetInt("fpsToggle", (fPSToggle.isOn ? 1 : 0));
        PlayerPrefs.SetFloat("fpsRefreshRate", refreshRateSec);

        PlayerPrefs.SetInt("audioSyncToggle", (audioSyncToggle.isOn ? 1 : 0));

        PlayerPrefs.SetInt("aoToggle", (aOToggle.isOn ? 1 : 0));
        PlayerPrefs.SetInt("bloomToggle", (bloomToggle.isOn ? 1 : 0));
        PlayerPrefs.SetInt("cgToggle", (cGToggle.isOn ? 1 : 0));
        PlayerPrefs.SetInt("chrAToggle", (chrAToggle.isOn ? 1 : 0));
        PlayerPrefs.SetInt("dofToggle", (dOFToggle.isOn ? 1 : 0));
        PlayerPrefs.SetInt("grainToggle", (grainToggle.isOn ? 1 : 0));
        PlayerPrefs.SetInt("lensDistToggle", (lensDistToggle.isOn ? 1 : 0));
        PlayerPrefs.SetInt("motBlurToggle", (motBlurToggle.isOn ? 1 : 0));
        PlayerPrefs.SetInt("ssrToggle", (sSRTogle.isOn ? 1 : 0));
        PlayerPrefs.SetInt("vinToggle", (vinToggle.isOn ? 1 : 0));

        AudioManager.instance.SetAudioVolumeLevel("default");
    }

    private void ResetSettings()
    {
        PlayerPrefs.DeleteKey("isFirstRun");
        PlayerPrefs.DeleteKey("qualitySetting");
        PlayerPrefs.DeleteKey("maxRenderDistance");
        PlayerPrefs.DeleteKey("mouseParallax");
        PlayerPrefs.DeleteKey("audioVolume");
        PlayerPrefs.DeleteKey("fpsToggle");
        PlayerPrefs.DeleteKey("fpsRefreshRate");
        PlayerPrefs.DeleteKey("audioSyncToggle");

        PlayerPrefs.DeleteKey("aoToggle");
        PlayerPrefs.DeleteKey("bloomToggle");
        PlayerPrefs.DeleteKey("cgToggle");
        PlayerPrefs.DeleteKey("chrAToggle");
        PlayerPrefs.DeleteKey("dofToggle");
        PlayerPrefs.DeleteKey("grainToggle");
        PlayerPrefs.DeleteKey("lensDistToggle");
        PlayerPrefs.DeleteKey("motBlurToggle");
        PlayerPrefs.DeleteKey("ssrToggle");
        PlayerPrefs.DeleteKey("vinToggle");

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
                string tempQuality = QualitySettings.names[((int)qualitySlider.value)];
                qualitySelectedLabel.transform.rotation = Quaternion.Euler(0, 0, 0);

                switch (tempQuality)
                {
                    case "Very Low":
                        string[] randomStrs = { "Ultra Budget!", "Junky!", "Trashy!", "Upgrade PC!", "Throw PC!", "Sorry, I guess?", "Have Fun LOL!" };
                        qualitySelectedLabel.text = randomStrs[Random.Range(0, randomStrs.Length)];
                        break;
                    case "Ultra":
                        qualitySelectedLabel.text = $"Plus {tempQuality}!";
                        qualitySelectedLabel.transform.Rotate(0, 0, 4, Space.World);
                        break;
                    default:
                        qualitySelectedLabel.text = tempQuality;
                        break;
                }

                break;

            case "render distance":
                float tempRenderDist = maxRenderDistSlider.value;
                renderDistSelectedLabel.transform.rotation = Quaternion.Euler(0, 0, 0);

                switch (tempRenderDist)
                {
                    case 30f:
                        string[] randomStrs = { "Ultra Budget!", "Junky!", "Trashy!", "Upgrade PC!", "Throw PC!", "Sorry, I guess?", "Have Fun LOL!" };
                        renderDistSelectedLabel.text = randomStrs[Random.Range(0, randomStrs.Length)];
                        break;
                    case 200f:
                        renderDistSelectedLabel.text = "MAX!";
                        renderDistSelectedLabel.transform.Rotate(0, 0, 4, Space.World);
                        break;
                    default:
                        renderDistSelectedLabel.text = maxRenderDistSlider.value.ToString() + " meters";
                        break;
                }

                break;

            case "mouse parallax":
                int tempMouseParallaxSetting = ((int)mouseParallaxSlider.value);
                string tempStr = "";
                mouseParallaxSelectedLabel.transform.rotation = Quaternion.Euler(0, 0, 0);

                switch (tempMouseParallaxSetting)
                {
                    case 0:
                        tempStr = "OFF";
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
                        tempStr = "Shake That Mouse!";
                        mouseParallaxSelectedLabel.transform.Rotate(0, 0, 4, Space.World);
                        break;
                    default:
                        Debug.LogError("Something went wrong in the script [Settings.cs]... -> Go to RefreshLabel() -> Mouse Parallax");
                        break;
                }

                mouseParallaxSelectedLabel.text = tempStr;
                break;

            case "volume":
                float tempVol = audioVolSlider.value;
                volumeSelectedLabel.transform.rotation = Quaternion.Euler(0, 0, 0);

                switch (tempVol)
                {
                    case 0f:
                        volumeSelectedLabel.text = "OFF";
                        break;
                    case 100f:
                        volumeSelectedLabel.text = "MAX!";
                        volumeSelectedLabel.transform.Rotate(0, 0, 4, Space.World);
                        break;
                    default:
                        volumeSelectedLabel.text = tempVol + "%";
                        break;
                }

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
            Debug.Log("--- GENERAL SETTINGS LOADED ---");

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
            Debug.Log($"Audio Volume: {PlayerPrefs.GetFloat("audioVolume")}%");
            Debug.Log($"FPS Display: {PlayerPrefs.GetInt("fpsToggle") != 0}");
            Debug.Log($"FPS Refresh Rate: {PlayerPrefs.GetFloat("fpsRefreshRate")} second(s)");

            bool tempAudioSyncToggle = PlayerPrefs.GetInt("audioSyncToggle") != 0;
            if (tempAudioSyncToggle)
            {
                Debug.Log("Reduce Audio Delay is turned ON");
            }
            else
            {
                Debug.Log("Reduce Audio Delay is turned OFF");
            }

            Debug.Log("--- POST PROCESSING SETTINGS LOADED ---");

            Debug.Log($"AO: {PlayerPrefs.GetInt("aoToggle") != 0}");
            Debug.Log($"Bloom: {PlayerPrefs.GetInt("bloomToggle") != 0}");
            Debug.Log($"Color Grading: {PlayerPrefs.GetInt("cgToggle") != 0}");
            Debug.Log($"Chromatic Aberration: {PlayerPrefs.GetInt("chrAToggle") != 0}");
            Debug.Log($"Depth of Field: {PlayerPrefs.GetInt("dofToggle") != 0}");
            Debug.Log($"Grain: {PlayerPrefs.GetInt("grainToggle") != 0}");
            Debug.Log($"Lens Distortion: {PlayerPrefs.GetInt("lensDistToggle") != 0}");
            Debug.Log($"Motion Blur: {PlayerPrefs.GetInt("motBlurToggle") != 0}");
            Debug.Log($"Screen Space Reflections: {PlayerPrefs.GetInt("ssrToggle") != 0}");
            Debug.Log($"Vignette: {PlayerPrefs.GetInt("vinToggle") != 0}");
        }
    }
}
