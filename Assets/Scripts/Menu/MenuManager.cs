using UnityEngine;
using RaahiFishing.Core;
using RaahiFishing.Audio;

namespace RaahiFishing.Menu
{
    /// <summary>
    /// Manages main menu interactions
    /// Single Responsibility: Menu flow control
    /// Depends on abstractions (SceneLoader, AudioManager)
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private GameObject loadingPanel;
        [SerializeField] private string fishingSceneName = "FishingGame";

        private void Start()
        {
            ShowMainPanel();
        }

        public void OnPlayButtonClicked()
        {
            PlayButtonSound();
            ShowLoadingPanel();
            
            if (SceneLoader.Instance != null)
            {
                SceneLoader.Instance.LoadSceneAsync(
                    fishingSceneName,
                    onProgress: (progress) => {
                        // Progress is handled by LoadingUI
                    },
                    onComplete: () => {
                        // Scene loaded
                    }
                );
            }
            else
            {
                Debug.LogError("SceneLoader instance not found!");
            }
        }

        public void OnSettingsButtonClicked()
        {
            PlayButtonSound();
            ShowSettingsPanel();
        }

        public void OnBackButtonClicked()
        {
            PlayButtonSound();
            ShowMainPanel();
        }

        public void OnQuitButtonClicked()
        {
            PlayButtonSound();
            
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            Debug.Log("Quit button pressed - stopping play mode");
            #else
            Application.Quit();
            #endif
        }

        private void ShowMainPanel()
        {
            mainPanel?.SetActive(true);
            settingsPanel?.SetActive(false);
            loadingPanel?.SetActive(false);
        }

        private void ShowSettingsPanel()
        {
            mainPanel?.SetActive(false);
            settingsPanel?.SetActive(true);
            loadingPanel?.SetActive(false);
        }

        private void ShowLoadingPanel()
        {
            mainPanel?.SetActive(false);
            settingsPanel?.SetActive(false);
            loadingPanel?.SetActive(true);
        }

        private void PlayButtonSound()
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayButtonClick();
            }
        }
    }
}
