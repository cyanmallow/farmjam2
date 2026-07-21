using UnityEngine;
using UnityEngine.UIElements;

public class NotClearedState : State
{
    public NotClearedState(FarmPlotState farmPlotState) : base(farmPlotState)
    {
    }
    public override void OnEnter()
    {
        // Implementation for entering the not cleared state
        Debug.Log("Entering Not Cleared State");
    }
    public override void OnUpdate()
    {
        // Implementation for updating the not cleared state
    }
    public override void OnExit()
    {
        // Implementation for exiting the not cleared state
        Debug.Log("Exiting Not Cleared State");
    }
}
