using UnityEngine;
using UnityEngine.UIElements;

public class HarvestState : State
{
    public HarvestState(StateMachineManager manager) : base(manager)
    {
    }
    public override void EnterState()
    {
        // Implementation for entering the harvest state
        Debug.Log("Entering Harvest State");

    }
    public override void UpdateState()
    {
        // Implementation for updating the harvest state
    }
    public override void ExitState()
    {
        // Implementation for exiting the harvest state
        Debug.Log("Exiting Harvest State");
    }
}
