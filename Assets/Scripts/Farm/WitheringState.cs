using System;
using UnityEngine;
using UnityEngine.UIElements;

public class WitheringState : State
{
    private bool isWatered;

    public WitheringState(FarmPlotState farmPlotState) : base(farmPlotState)    {    }

    private void Dying()
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

    public override void OnEnter()
    {
        isWatered = false;
        DayMonthManager.OnNewDay += Dying;
        Debug.Log("Entering Withering State");

    }
    public override void OnUpdate()
    {
        // Implementation for updating the withering state
    }
    public override void OnExit()
    {
        // Implementation for exiting the withering state
        DayMonthManager.OnNewDay -= Dying;
        Debug.Log("Exiting Withering State");
    }
    public override void Harvest()
    {
        farmPlotState.tile.HarvestWhilePlantDying();
        DayMonthManager.Instance.AddTime(2);
        farmPlotState.SwitchState(farmPlotState.EmptyStateInstance);
        StatManager.Instance.AddStat("fatigue", 10);

    }
}
