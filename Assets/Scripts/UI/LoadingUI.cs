using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RaahiFishing.Core;

namespace RaahiFishing.UI
{
    /// <summary>
    /// Displays loading progress during scene transitions
    /// </summary>
    public class LoadingUI : MonoBehaviour
    {
        [SerializeField] private Slider progressBar;
        [SerializeField] private TextMeshProUGUI progressText;
        [SerializeField] private GameObject spinner;

        private void OnEnable()
        {
            if (SceneLoader.Instance != null)
            {
                // Subscribe to progress updates would go here if we exposed events
            }
        }

        private void Update()
        {
            // Rotate spinner for visual feedback
            if (spinner != null)
            {
                spinner.transform.Rotate(0, 0, -180f * Time.deltaTime);
            }
        }

        public void UpdateProgress(float progress)
        {
            if (progressBar != null)
            {
                progressBar.value = progress;
            }

            if (progressText != null)
            {
                progressText.text = $"Loading... {Mathf.RoundToInt(progress * 100)}%";
            }
        }
    }
}
