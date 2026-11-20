using Unity.VisualScripting;
using UnityEngine;

public class GrowingState : State
{
    public bool isWatered;
    public GrowingState(FarmPlotState farmPlotState) : base(farmPlotState)    {    }
    public override string StateName => "growing";
    public override bool CanWater => !isWatered;
    public override bool CanClear => true;

    public override void OnEnter()
    {
        isWatered = false;
        DayMonthManager.OnNewDay += GrowUp;

    }
    public override void OnUpdate()
    {
        // Implementation for updating the growing state
    }
    public override void OnExit()
    {
        // Implementation for exiting the growing state
        DayMonthManager.OnNewDay -= GrowUp;

        Debug.Log("Exiting Growing State");
    }
    public override void Water()
    {
        isWatered = true;
        // TODO: change the time somewhere else, not here, because this is just the state, not the action
        DayMonthManager.Instance.AddTime(1);
        StatManager.Instance.AddStat("fatigue", 10);
    }
    public override void GrowUp()
    {
        if (!isWatered)
        {
            farmPlotState.tile.deadPoint--;
            if (farmPlotState.tile.CheckIfPlantIsDead())
            {
                farmPlotState.SwitchState(farmPlotState.DeadStateInstance);
                return;
            }
            return;
        }
        farmPlotState.tile.AdvanceGrowth();
        isWatered = false;
        if (farmPlotState.tile.IsFullyGrown == true)
        {
            farmPlotState.SwitchState(farmPlotState.HarvestStateInstance);
        }
    }
    public override void Clear()
    {
        farmPlotState.SwitchState(farmPlotState.EmptyStateInstance);
    }
}
