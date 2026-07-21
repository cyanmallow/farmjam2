using UnityEngine;
using UnityEngine.UIElements;

public class NotClearedState : State
{
    private int timesLeftToClear; // Number of times the player needs spend to clear the plot
    public NotClearedState(FarmPlotState farmPlotState) : base(farmPlotState)
    {
    }
    public override void OnEnter()
    {
        // Implementation for entering the not cleared state
        Debug.Log("Entering Not Cleared State, clear 5 times to plant");
        timesLeftToClear = 5;
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
