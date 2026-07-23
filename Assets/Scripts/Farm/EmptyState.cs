using UnityEngine;
using UnityEngine.UIElements;

public class EmptyState : State
{
    public EmptyState(FarmPlotState farmPlotState) : base(farmPlotState)    {    }
    public override void OnEnter()
    {
        Debug.Log("Field is empty");

    }
    public override void Plant(SeedData seed)
    {
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
