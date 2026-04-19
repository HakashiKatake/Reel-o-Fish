using UnityEngine;
using RaahiFishing.Core;
using RaahiFishing.UI;
using RaahiFishing.Fishing.Mechanics;

namespace RaahiFishing.Fishing.States
{
    /// <summary>
    /// Reeling state - player must complete the reel mechanic
    /// Delegates to IReelMechanic for flexibility
    /// </summary>
    public class ReelingState : IGameState
    {
        private readonly FishingManager fishingManager;
        private IReelMechanic reelMechanic;

        public ReelingState(FishingManager manager)
        {
            fishingManager = manager;
        }

        public void Enter()
        {
            if (FishingUI.Instance != null)
            {
                FishingUI.Instance.UpdateStateText("Reel it in! Tap SPACE!");
                FishingUI.Instance.ShowReelMechanic();
                
                // Get the reel mechanic from the panel
                GameObject reelPanel = FishingUI.Instance.ReelMechanicPanel;
                if (reelPanel != null)
                {
                    reelMechanic = reelPanel.GetComponent<IReelMechanic>();
                    
                    if (reelMechanic != null)
                    {
                        reelMechanic.Initialize(fishingManager.CurrentFish);
                    }
                }
            }
        }

        public void Execute()
        {
            if (reelMechanic != null)
            {
                ReelResult result = reelMechanic.UpdateMechanic();

                if (result == ReelResult.Success)
                {
                    fishingManager.OnCatchSuccess();
                }
                else if (result == ReelResult.Failure)
                {
                    fishingManager.OnCatchFailed();
                }
            }
        }

        public void Exit()
        {
            if (FishingUI.Instance != null)
            {
                FishingUI.Instance.HideReelMechanic();
            }
        }
    }
}
