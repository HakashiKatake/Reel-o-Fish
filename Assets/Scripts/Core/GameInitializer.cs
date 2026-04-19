using UnityEngine;
using RaahiFishing.Data;
using RaahiFishing.Audio;

namespace RaahiFishing.Core
{
    /// <summary>
    /// Initializes game settings when fishing scene loads
    /// Ensures settings from main menu are applied
    /// </summary>
    public class GameInitializer : MonoBehaviour
    {
        private void Start()
        {
            ApplySavedSettings();
            ApplyAudioSettings();
        }

        private void ApplySavedSettings()
        {
            GameSettings settings = new GameSettings();
            settings.ApplySettings();
            
            Debug.Log($"Applied settings - Quality: {settings.QualityLevel}, Volume: {settings.MasterVolume}");
        }

        private void ApplyAudioSettings()
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.ApplyVolume();
            }
            else
            {
                Debug.LogWarning("AudioManager instance not found. Audio settings not applied.");
            }
        }
    }
}
