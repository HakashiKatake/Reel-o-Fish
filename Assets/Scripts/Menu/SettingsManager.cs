using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RaahiFishing.Data;
using RaahiFishing.Audio;

namespace RaahiFishing.Menu
{
    /// <summary>
    /// Manages settings UI and applies changes
    /// Single Responsibility: Settings UI control
    /// Open/Closed Principle: Easy to extend with new settings
    /// </summary>
    public class SettingsManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private TMP_Dropdown qualityDropdown;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private TextMeshProUGUI volumeValueText;

        private GameSettings gameSettings;
        private Resolution[] resolutions;

        private void Awake()
        {
            gameSettings = new GameSettings();
        }

        private void Start()
        {
            InitializeResolutions();
            LoadSettings();
        }

        private void InitializeResolutions()
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            System.Collections.Generic.List<string> options = new System.Collections.Generic.List<string>();
            int currentResolutionIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        private void LoadSettings()
        {
            // Load volume
            volumeSlider.value = gameSettings.MasterVolume;
            UpdateVolumeText(gameSettings.MasterVolume);

            // Load quality
            qualityDropdown.value = gameSettings.QualityLevel;

            // Load resolution
            resolutionDropdown.value = gameSettings.ResolutionIndex;
        }

        public void OnVolumeChanged(float value)
        {
            gameSettings.MasterVolume = value;
            UpdateVolumeText(value);
            
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.ApplyVolume();
            }
        }

        public void OnQualityChanged(int index)
        {
            gameSettings.QualityLevel = index;
            QualitySettings.SetQualityLevel(index);
        }

        public void OnResolutionChanged(int index)
        {
            gameSettings.ResolutionIndex = index;
            Resolution resolution = resolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        private void UpdateVolumeText(float value)
        {
            if (volumeValueText != null)
            {
                volumeValueText.text = Mathf.RoundToInt(value * 100) + "%";
            }
        }
    }
}
