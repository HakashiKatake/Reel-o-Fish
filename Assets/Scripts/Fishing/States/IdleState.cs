using UnityEngine;
using RaahiFishing.Core;
using RaahiFishing.UI;
using RaahiFishing.Audio;

namespace RaahiFishing.Fishing.States
{
    /// <summary>
    /// Idle state - waiting for player to cast
    /// </summary>
    public class IdleState : IGameState
    {
        private readonly FishingManager fishingManager;

        public IdleState(FishingManager manager)
        {
            fishingManager = manager;
        }

        public void Enter()
        {
            if (FishingUI.Instance != null)
            {
                FishingUI.Instance.UpdateStateText("Press [SPACE] to Cast");
                FishingUI.Instance.HideReelMechanic();
            }
        }

        public void Execute()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlayCastSound();
                }
                fishingManager.StartCasting();
            }
        }

        public void Exit()
        {
            // Cleanup if needed
        }
    }
}
