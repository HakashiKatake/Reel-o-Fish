using RaahiFishing.Data;

namespace RaahiFishing.Fishing.Mechanics
{
    /// <summary>
    /// Interface for different reel mechanics
    /// Open/Closed Principle: Easy to add new mechanics without modifying existing code
    /// </summary>
    public interface IReelMechanic
    {
        void Initialize(FishData fish);
        ReelResult UpdateMechanic();
    }

    public enum ReelResult
    {
        InProgress,
        Success,
        Failure
    }
}
