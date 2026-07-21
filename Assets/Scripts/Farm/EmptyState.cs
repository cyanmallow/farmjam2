using UnityEngine;
using UnityEngine.UIElements;

public class EmptyState : State
{
    public EmptyState(FarmPlotState farmPlotState) : base(farmPlotState)
    {
    }
    public override void OnEnter()
    {
        // Implementation for entering the empty state
        Debug.Log("Field is empty");

    }
    public override void OnUpdate()
    {
        // Implementation for updating the empty state
    }
    public override void OnExit()
    {
        // Implementation for exiting the empty state
        Debug.Log("Exiting Empty State");
    }
}
