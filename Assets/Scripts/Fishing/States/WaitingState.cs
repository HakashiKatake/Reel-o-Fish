using UnityEngine;
using RaahiFishing.Core;
using RaahiFishing.UI;
using RaahiFishing.Audio;

namespace RaahiFishing.Fishing.States
{
    /// <summary>
    /// Waiting state - line is cast, waiting for a bite
    /// </summary>
    public class WaitingState : IGameState
    {
        private readonly FishingManager fishingManager;
        private readonly float minWaitTime;
        private readonly float maxWaitTime;
        
        private float waitTimer;
        private float targetWaitTime;

        public WaitingState(FishingManager manager, float minWait, float maxWait)
        {
            fishingManager = manager;
            minWaitTime = minWait;
            maxWaitTime = maxWait;
        }

        public void Enter()
        {
            waitTimer = 0f;
            targetWaitTime = Random.Range(minWaitTime, maxWaitTime);
            
            if (FishingUI.Instance != null)
            {
                FishingUI.Instance.UpdateStateText("Waiting for a bite...");
                FishingUI.Instance.HideReelMechanic();
            }

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySplashSound();
            }
        }

        public void Execute()
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= targetWaitTime)
            {
                fishingManager.OnBiteDetected();
            }
        }

        public void Exit()
        {
            // Cleanup
        }
    }
}
