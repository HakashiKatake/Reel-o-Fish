using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RaahiFishing.Data;
using System.Collections.Generic;

namespace RaahiFishing.UI
{
    /// <summary>
    /// Manages fishing game UI
    /// Single Responsibility: UI display and updates
    /// </summary>
    public class FishingUI : MonoBehaviour
    {
        public static FishingUI Instance { get; private set; }

        [Header("State Display")]
        [SerializeField] private TextMeshProUGUI stateText;

        [Header("Reel Mechanic UI")]
        [SerializeField] private GameObject reelMechanicPanel;

        [Header("Catch Log")]
        [SerializeField] private Transform catchLogContainer;
        [SerializeField] private GameObject catchLogItemPrefab;
        [SerializeField] private TextMeshProUGUI catchCountText;

        private List<FishData> caughtFish = new List<FishData>();

        // Public property to access the reel mechanic panel
        public GameObject ReelMechanicPanel => reelMechanicPanel;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            HideReelMechanic();
            UpdateCatchCount();
        }

        public void UpdateStateText(string text)
        {
            if (stateText != null)
            {
                stateText.text = text;
            }
        }

        public void SetStateTextColor(Color color)
        {
            if (stateText != null)
            {
                stateText.color = color;
            }
        }

        public void ShowReelMechanic()
        {
            if (reelMechanicPanel != null)
            {
                reelMechanicPanel.SetActive(true);
            }
        }

        public void HideReelMechanic()
        {
            if (reelMechanicPanel != null)
            {
                reelMechanicPanel.SetActive(false);
            }
        }

        public void AddCatchToLog(FishData fish)
        {
            if (fish == null) return;

            caughtFish.Add(fish);
            UpdateCatchCount();

            // Create log entry if prefab exists
            if (catchLogItemPrefab != null && catchLogContainer != null)
            {
                GameObject logItem = Instantiate(catchLogItemPrefab, catchLogContainer);
                CatchLogItem itemScript = logItem.GetComponent<CatchLogItem>();
                
                if (itemScript != null)
                {
                    itemScript.Setup(fish);
                }
            }
        }

        private void UpdateCatchCount()
        {
            if (catchCountText != null)
            {
                catchCountText.text = $"Caught: {caughtFish.Count}";
            }
        }

        public int GetCatchCount()
        {
            return caughtFish.Count;
        }
    }
}
