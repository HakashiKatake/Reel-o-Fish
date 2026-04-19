using UnityEngine;

namespace RaahiFishing.Data
{
    /// <summary>
    /// Manages game settings persistence using PlayerPrefs
    /// Single Responsibility: Settings data management
    /// </summary>
    public class GameSettings
    {
        private const string MASTER_VOLUME_KEY = "MasterVolume";
        private const string QUALITY_LEVEL_KEY = "QualityLevel";
        private const string RESOLUTION_INDEX_KEY = "ResolutionIndex";

        public float MasterVolume
        {
            get => PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 1f);
            set
            {
                PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, Mathf.Clamp01(value));
                PlayerPrefs.Save();
            }
        }

        public int QualityLevel
        {
            get => PlayerPrefs.GetInt(QUALITY_LEVEL_KEY, QualitySettings.GetQualityLevel());
            set
            {
                PlayerPrefs.SetInt(QUALITY_LEVEL_KEY, value);
                PlayerPrefs.Save();
            }
        }

        public int ResolutionIndex
        {
            get => PlayerPrefs.GetInt(RESOLUTION_INDEX_KEY, Screen.resolutions.Length - 1);
            set
            {
                PlayerPrefs.SetInt(RESOLUTION_INDEX_KEY, value);
                PlayerPrefs.Save();
            }
        }

        public void ApplySettings()
        {
            QualitySettings.SetQualityLevel(QualityLevel);
            
            Resolution[] resolutions = Screen.resolutions;
            if (ResolutionIndex >= 0 && ResolutionIndex < resolutions.Length)
            {
                Resolution resolution = resolutions[ResolutionIndex];
                Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            }
        }
    }
}
