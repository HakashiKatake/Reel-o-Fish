using UnityEngine;
using RaahiFishing.Core;
using RaahiFishing.Fishing.States;
using RaahiFishing.Data;

namespace RaahiFishing.Fishing
{
    /// <summary>
    /// Main controller for the fishing game
    /// Implements State Pattern for clean state management
    /// Single Responsibility: Coordinate fishing states
    /// </summary>
    public class FishingManager : MonoBehaviour
    {
        public static FishingManager Instance { get; private set; }

        [Header("Fish Database")]
        [SerializeField] private FishData[] availableFish;

        [Header("Timing Settings")]
        [SerializeField] private float minWaitTime = 2f;
        [SerializeField] private float maxWaitTime = 6f;

        // Current state
        private IGameState currentState;
        
        // State instances
        private IdleState idleState;
        private WaitingState waitingState;
        private BiteState biteState;
        private ReelingState reelingState;
        private ResultState resultState;

        // Current fish being caught
        public FishData CurrentFish { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            InitializeStates();
        }

        private void Start()
        {
            ChangeState(idleState);
        }

        private void Update()
        {
            currentState?.Execute();
        }

        private void InitializeStates()
        {
            idleState = new IdleState(this);
            waitingState = new WaitingState(this, minWaitTime, maxWaitTime);
            biteState = new BiteState(this);
            reelingState = new ReelingState(this);
            resultState = new ResultState(this);
        }

        public void ChangeState(IGameState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }

        // State transition methods
        public void StartCasting()
        {
            SelectRandomFish();
            ChangeState(waitingState);
        }

        public void OnBiteDetected()
        {
            ChangeState(biteState);
        }

        public void StartReeling()
        {
            ChangeState(reelingState);
        }

        public void OnCatchSuccess()
        {
            ChangeState(resultState);
        }

        public void OnCatchFailed()
        {
            CurrentFish = null;
            ChangeState(resultState);
        }

        public void ReturnToIdle()
        {
            ChangeState(idleState);
        }

        private void SelectRandomFish()
        {
            if (availableFish == null || availableFish.Length == 0)
            {
                Debug.LogError("No fish available in the database!");
                return;
            }

            // Calculate total probability
            float totalProbability = 0f;
            foreach (var fish in availableFish)
            {
                totalProbability += fish.spawnProbability;
            }

            // Random selection based on probability
            float randomValue = Random.Range(0f, totalProbability);
            float cumulativeProbability = 0f;

            foreach (var fish in availableFish)
            {
                cumulativeProbability += fish.spawnProbability;
                if (randomValue <= cumulativeProbability)
                {
                    CurrentFish = fish;
                    return;
                }
            }

            // Fallback to first fish
            CurrentFish = availableFish[0];
        }

        // Getters for states (used by state classes)
        public IdleState GetIdleState() => idleState;
        public WaitingState GetWaitingState() => waitingState;
        public BiteState GetBiteState() => biteState;
        public ReelingState GetReelingState() => reelingState;
        public ResultState GetResultState() => resultState;
    }
}
