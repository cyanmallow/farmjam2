using UnityEngine;
using UnityEngine.UIElements;

public class EmptyState : State
{
    public EmptyState(StateMachineManager manager) : base(manager)
    {
    }
    public override void EnterState()
    {
        // Implementation for entering the empty state
        Debug.Log("Field is empty");

    }
    public override void UpdateState()
    {
        // Implementation for updating the empty state
    }
    public override void ExitState()
    {
        // Implementation for exiting the empty state
        Debug.Log("Exiting Empty State");
    }
}
