using UnityEngine;
using UnityEngine.UIElements;

public class HarvestState : State
{
    private int countdownToWither = 2;
    public HarvestState(FarmPlotState farmPlotState) : base(farmPlotState)    {    }
    public override void OnEnter()
    {
        // Implementation for entering the harvest state
        Debug.Log("Entering Harvest State");
        DayMonthManager.OnNewDay += GrowUp;
        farmPlotState.tile.deadPoint += 2;
    }
    public override void OnUpdate()
    {
        // Implementation for updating the harvest state
    }
    public override void OnExit()
    {
        // Implementation for exiting the harvest state
        Debug.Log("Exiting Harvest State");
        DayMonthManager.OnNewDay -= GrowUp;
    }
    public override void Harvest()
    {
        farmPlotState.tile.HarvestWhilePlantNotDead();
        DayMonthManager.Instance.AddTime(2);
        farmPlotState.SwitchState(farmPlotState.EmptyStateInstance);
        StatManager.Instance.AddStat("fatigue", 10);

    }

    public override void GrowUp()
    {
        countdownToWither--;
        if (countdownToWither <= 0)
        {
            farmPlotState.SwitchState(farmPlotState.WitheringStateInstance);
        }
    }
}
