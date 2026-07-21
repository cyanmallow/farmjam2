using UnityEngine;
using UnityEngine.UIElements;

public class DeadState : State
{
    public DeadState(FarmPlotState farmPlotState) : base(farmPlotState)
    {
    }
    public override void OnEnter()
    {
        // Implementation for entering the dead state
        Debug.Log("Entering Dead State");

    }
    public override void OnUpdate()
    {
        // Implementation for updating the dead state
    }
    public override void OnExit()
    {
        // Implementation for exiting the dead state
        Debug.Log("Exiting Dead State");
    }
}
