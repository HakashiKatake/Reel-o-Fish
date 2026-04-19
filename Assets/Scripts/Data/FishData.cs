using UnityEngine;

namespace RaahiFishing.Data
{
    /// <summary>
    /// ScriptableObject defining fish properties
    /// Data-driven design for easy fish creation
    /// </summary>
    [CreateAssetMenu(fileName = "New Fish", menuName = "Raahi Fishing/Fish Data")]
    public class FishData : ScriptableObject
    {
        [Header("Basic Info")]
        public string fishName = "Unknown Fish";
        public Sprite fishIcon;
        
        [Header("Rarity")]
        public FishRarity rarity = FishRarity.Common;
        [Range(0f, 1f)]
        [Tooltip("Probability of this fish appearing (0-1)")]
        public float spawnProbability = 0.5f;
        
        [Header("Difficulty")]
        public FishDifficulty difficulty = FishDifficulty.Easy;
        
        [Header("Reel Mechanics")]
        [Tooltip("Speed multiplier for the tension bar or reel mechanic")]
        [Range(0.5f, 3f)]
        public float reelSpeedMultiplier = 1f;
        
        [Tooltip("Time window to react to the bite (seconds)")]
        [Range(0.5f, 3f)]
        public float reactionWindow = 1.5f;
        
        [Tooltip("Number of successful hits needed to catch (for timing ring mechanic)")]
        [Range(1, 5)]
        public int hitsRequired = 3;
    }

    public enum FishRarity
    {
        Common,
        Uncommon,
        Rare
    }

    public enum FishDifficulty
    {
        Easy,
        Medium,
        Hard
    }
}
