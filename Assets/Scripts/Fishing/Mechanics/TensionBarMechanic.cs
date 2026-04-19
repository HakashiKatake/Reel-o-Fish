using UnityEngine;
using UnityEngine.UI;
using RaahiFishing.Data;
using RaahiFishing.Audio;

namespace RaahiFishing.Fishing.Mechanics
{
    /// <summary>
    /// Tension bar reel mechanic
    /// Player must keep tension in the sweet spot by tapping Space
    /// </summary>
    public class TensionBarMechanic : MonoBehaviour, IReelMechanic
    {
        [Header("UI References")]
        [SerializeField] private Slider tensionSlider;
        [SerializeField] private Image fillImage;
        [SerializeField] private RectTransform sweetSpotIndicator;

        [Header("Settings")]
        [SerializeField] private float tensionIncreaseRate = 0.3f;
        [SerializeField] private float tensionDecreaseRate = 0.15f;
        [SerializeField] private float sweetSpotSize = 0.2f;
        [SerializeField] private float successDuration = 3f;

        private float currentTension;
        private float sweetSpotCenter;
        private float speedMultiplier;
        private float successTimer;
        private bool isActive;

        public void Initialize(FishData fish)
        {
            isActive = true;
            currentTension = 0.5f;
            successTimer = 0f;
            sweetSpotCenter = Random.Range(0.3f, 0.7f);
            speedMultiplier = fish != null ? fish.reelSpeedMultiplier : 1f;

            if (tensionSlider != null)
            {
                tensionSlider.value = currentTension;
            }

            UpdateSweetSpotPosition();
        }

        public ReelResult UpdateMechanic()
        {
            if (!isActive) return ReelResult.InProgress;

            // Tension naturally increases over time
            currentTension += tensionIncreaseRate * speedMultiplier * Time.deltaTime;

            // Player taps Space to reduce tension
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentTension -= tensionDecreaseRate;
                
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlayReelSound();
                }
            }

            // Clamp tension
            currentTension = Mathf.Clamp01(currentTension);

            // Update UI
            if (tensionSlider != null)
            {
                tensionSlider.value = currentTension;
            }

            // Check if in sweet spot
            float sweetSpotMin = sweetSpotCenter - sweetSpotSize / 2f;
            float sweetSpotMax = sweetSpotCenter + sweetSpotSize / 2f;

            if (currentTension >= sweetSpotMin && currentTension <= sweetSpotMax)
            {
                // In sweet spot - progress towards success
                successTimer += Time.deltaTime;
                UpdateFillColor(Color.green);

                if (successTimer >= successDuration)
                {
                    isActive = false;
                    return ReelResult.Success;
                }
            }
            else
            {
                // Outside sweet spot - reset progress
                successTimer = Mathf.Max(0, successTimer - Time.deltaTime * 0.5f);
                UpdateFillColor(Color.yellow);
            }

            // Check failure conditions
            if (currentTension >= 1f)
            {
                // Line snapped!
                isActive = false;
                return ReelResult.Failure;
            }
            else if (currentTension <= 0f)
            {
                // Fish escaped!
                isActive = false;
                return ReelResult.Failure;
            }

            return ReelResult.InProgress;
        }

        private void UpdateSweetSpotPosition()
        {
            if (sweetSpotIndicator != null && tensionSlider != null)
            {
                RectTransform sliderRect = tensionSlider.GetComponent<RectTransform>();
                float sliderWidth = sliderRect.rect.width;
                float xPos = (sweetSpotCenter - 0.5f) * sliderWidth;
                
                sweetSpotIndicator.anchoredPosition = new Vector2(xPos, sweetSpotIndicator.anchoredPosition.y);
            }
        }

        private void UpdateFillColor(Color color)
        {
            if (fillImage != null)
            {
                fillImage.color = color;
            }
        }
    }
}
