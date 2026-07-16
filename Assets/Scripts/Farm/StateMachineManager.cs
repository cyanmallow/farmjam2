using NUnit.Framework.Interfaces;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class StateMachineManager : MonoBehaviour
{
    public EmptyState EmptyStateInstance { get; private set; }
    public GrowingState GrowingStateInstance { get; private set; }
    public HarvestState HarvestStateInstance { get; private set; }

    public State _currentState;

    private void Awake()
    {
        EmptyStateInstance = new EmptyState(this);
        GrowingStateInstance = new GrowingState(this);
        HarvestStateInstance = new HarvestState(this);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchState(EmptyStateInstance);
    }

    // Update is called once per frame
    void Update()
    {
        //_currentState?.UpdateState();
    }
    public void SwitchState(State newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }
}
