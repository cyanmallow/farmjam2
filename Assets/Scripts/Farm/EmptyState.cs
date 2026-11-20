using UnityEngine;

public class EmptyState : State
{
    public EmptyState(FarmPlotState farmPlotState) : base(farmPlotState)    {    }
    public override string StateName => "empty";
    public override bool CanPlant => true;
    public override void OnEnter()
    {
        Debug.Log("Field is empty");
        // if the tile is not empty, clear it
        farmPlotState.tile.ClearField();
    }
    public override void Plant(SeedData seed)
    {
        // plant and switch state
        farmPlotState.tile.StartGrowing(seed);
        DayMonthManager.Instance.AddTime(2);
        farmPlotState.SwitchState(farmPlotState.GrowingStateInstance);
        StatManager.Instance.AddStat("fatigue", 10);
    }
    public override void OnExit()
    {
        // Implementation for exiting the empty state
        Debug.Log("Exiting Empty State");
    }

    public override void OnUpdate()
    {
        
    }
}
