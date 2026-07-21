using UnityEngine;
using UnityEngine.UIElements;

public class HarvestState : State
{
    public HarvestState(FarmPlotState farmPlotState) : base(farmPlotState)
    {
    }
    public override void OnEnter()
    {
        // Implementation for entering the harvest state
        Debug.Log("Entering Harvest State");

    }
    public override void OnUpdate()
    {
        // Implementation for updating the harvest state
    }
    public override void OnExit()
    {
        // Implementation for exiting the harvest state
        Debug.Log("Exiting Harvest State");
    }
}
