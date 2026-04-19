using UnityEngine;
using RaahiFishing.Core;
using RaahiFishing.Audio;

namespace RaahiFishing.UI
{
    /// <summary>
    /// Handles returning to main menu from fishing game
    /// </summary>
    public class BackToMenuButton : MonoBehaviour
    {
        [SerializeField] private string mainMenuSceneName = "MainMenu";

        public void OnBackToMenuClicked()
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayButtonClick();
            }

            if (SceneLoader.Instance != null)
            {
                SceneLoader.Instance.LoadSceneAsync(mainMenuSceneName);
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuSceneName);
            }
        }

        private void Update()
        {
            // Allow ESC key to return to menu
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnBackToMenuClicked();
            }
        }
    }
}
