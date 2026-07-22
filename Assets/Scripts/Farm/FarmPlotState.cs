using NUnit.Framework.Interfaces;
using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class FarmPlotState : MonoBehaviour
{
    public NotClearedState NotClearedStateInstance { get; private set; }
    public EmptyState EmptyStateInstance { get; private set; }
    public GrowingState GrowingStateInstance { get; private set; }
    public HarvestState HarvestStateInstance { get; private set; }
    public DeadState DeadStateInstance { get; private set; }

    public State CurrentState { get; private set; }
    public event Action<State> OnStateChanged;
    public Tile tile;

    private void Awake()
    {
        NotClearedStateInstance = new NotClearedState(this);
        EmptyStateInstance = new EmptyState(this);
        GrowingStateInstance = new GrowingState(this);
        HarvestStateInstance = new HarvestState(this);
        DeadStateInstance = new DeadState(this);

        tile = GetComponent<Tile>();
    }
    void Start()
    {
        SwitchState(EmptyStateInstance);
    }

    void Update()
    {
        //_currentState?.OnUpdate();
    }
    public void SwitchState(State newState)
    {
        CurrentState?.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
        OnStateChanged?.Invoke(CurrentState);
    }

    public State GetCurrentFieldState()
    {
        return CurrentState;
    }
}
