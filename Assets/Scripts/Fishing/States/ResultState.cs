using UnityEngine;
using RaahiFishing.Core;
using RaahiFishing.UI;
using RaahiFishing.Audio;

namespace RaahiFishing.Fishing.States
{
    /// <summary>
    /// Result state - shows catch result and waits for player input
    /// </summary>
    public class ResultState : IGameState
    {
        private readonly FishingManager fishingManager;
        private float displayTimer;
        private const float DISPLAY_DURATION = 2f;

        public ResultState(FishingManager manager)
        {
            fishingManager = manager;
        }

        public void Enter()
        {
            displayTimer = 0f;

            if (fishingManager.CurrentFish != null)
            {
                // Success!
                if (FishingUI.Instance != null)
                {
                    FishingUI.Instance.UpdateStateText($"Caught: {fishingManager.CurrentFish.fishName}!");
                    FishingUI.Instance.SetStateTextColor(Color.green);
                    FishingUI.Instance.AddCatchToLog(fishingManager.CurrentFish);
                }

                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySuccessSound();
                }
            }
            else
            {
                // Failed
                if (FishingUI.Instance != null)
                {
                    FishingUI.Instance.UpdateStateText("The fish got away!");
                    FishingUI.Instance.SetStateTextColor(Color.red);
                }

                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlayFailSound();
                }
            }
        }

        public void Execute()
        {
            displayTimer += Time.deltaTime;

            if (displayTimer >= DISPLAY_DURATION)
            {
                fishingManager.ReturnToIdle();
            }
        }

        public void Exit()
        {
            if (FishingUI.Instance != null)
            {
                FishingUI.Instance.SetStateTextColor(Color.white);
            }
        }
    }
}
