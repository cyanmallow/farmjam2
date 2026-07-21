using NUnit.Framework.Interfaces;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class FarmPlotState : MonoBehaviour
{
    public NotClearedState notClearedStateInstance { get; private set; }
    public EmptyState EmptyStateInstance { get; private set; }
    public GrowingState GrowingStateInstance { get; private set; }
    public HarvestState HarvestStateInstance { get; private set; }
    public DeadState DeadStateInstance { get; private set; }

    public State _currentState;

    private void Awake()
    {
        notClearedStateInstance = new NotClearedState(this);
        EmptyStateInstance = new EmptyState(this);
        GrowingStateInstance = new GrowingState(this);
        HarvestStateInstance = new HarvestState(this);
        DeadStateInstance = new DeadState(this);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchState(notClearedStateInstance);
    }

    // Update is called once per frame
    void Update()
    {
        _currentState?.OnUpdate();
    }
    public void SwitchState(State newState)
    {
        _currentState?.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }

    public State GetCurrentFieldState()
    {
        Debug.Log($"Current state: {_currentState.GetType().Name}");
        return _currentState;
    }
}
