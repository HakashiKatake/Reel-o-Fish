using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RaahiFishing.Data;

namespace RaahiFishing.UI
{
    /// <summary>
    /// Individual catch log entry UI component
    /// </summary>
    public class CatchLogItem : MonoBehaviour
    {
        [SerializeField] private Image fishIcon;
        [SerializeField] private TextMeshProUGUI fishNameText;
        [SerializeField] private TextMeshProUGUI rarityText;

        public void Setup(FishData fish)
        {
            if (fish == null) return;

            if (fishIcon != null && fish.fishIcon != null)
            {
                fishIcon.sprite = fish.fishIcon;
            }

            if (fishNameText != null)
            {
                fishNameText.text = fish.fishName;
            }

            if (rarityText != null)
            {
                rarityText.text = fish.rarity.ToString();
                rarityText.color = GetRarityColor(fish.rarity);
            }
        }

        private Color GetRarityColor(FishRarity rarity)
        {
            switch (rarity)
            {
                case FishRarity.Common:
                    return Color.white;
                case FishRarity.Uncommon:
                    return Color.green;
                case FishRarity.Rare:
                    return new Color(1f, 0.5f, 0f); // Orange
                default:
                    return Color.white;
            }
        }
    }
}
