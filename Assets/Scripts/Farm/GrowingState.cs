using Unity.VisualScripting;
using UnityEngine;

public class GrowingState : State
{
    private bool isWatered;
    public GrowingState(FarmPlotState farmPlotState) : base(farmPlotState)    {    }
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
        DayMonthManager.Instance.AddTime(2);
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
}
