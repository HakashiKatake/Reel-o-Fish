using UnityEngine;

namespace RaahiFishing.Core
{
    /// <summary>
    /// Interface for game states following the State Pattern
    /// Adheres to Interface Segregation Principle
    /// </summary>
    public interface IGameState
    {
        void Enter();
        void Execute();
        void Exit();
    }
}
