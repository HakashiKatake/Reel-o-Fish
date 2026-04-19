using UnityEngine;
using RaahiFishing.Core;
using RaahiFishing.UI;
using RaahiFishing.Audio;

namespace RaahiFishing.Fishing.States
{
    /// <summary>
    /// Bite state - fish has bitten, player must react quickly
    /// </summary>
    public class BiteState : IGameState
    {
        private readonly FishingManager fishingManager;
        private float reactionTimer;
        private float reactionWindow;

        public BiteState(FishingManager manager)
        {
            fishingManager = manager;
        }

        public void Enter()
        {
            reactionTimer = 0f;
            
            // Get reaction window from current fish
            if (fishingManager.CurrentFish != null)
            {
                reactionWindow = fishingManager.CurrentFish.reactionWindow;
            }
            else
            {
                reactionWindow = 1.5f; // Default
            }

            if (FishingUI.Instance != null)
            {
                FishingUI.Instance.UpdateStateText("FISH ON! Press [SPACE]!");
                FishingUI.Instance.SetStateTextColor(Color.yellow);
            }

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayBiteSound();
            }
        }

        public void Execute()
        {
            reactionTimer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Player reacted in time!
                fishingManager.StartReeling();
            }
            else if (reactionTimer >= reactionWindow)
            {
                // Player missed the window
                fishingManager.OnCatchFailed();
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
