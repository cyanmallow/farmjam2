using Unity.VisualScripting;
using UnityEngine;

public class GrowingState : State
{
    private bool isWatered;
    [SerializeField] private Renderer renderer;
    public GrowingState(FarmPlotState farmPlotState) : base(farmPlotState)    {    }
    public override void OnEnter()
    {
        isWatered = false;
    }
    public override void OnUpdate()
    {
        // Implementation for updating the growing state
    }
    public override void OnExit()
    {
        // Implementation for exiting the growing state
        Debug.Log("Exiting Growing State");
    }
    public override void Water()
    {
        farmPlotState.tile.Water();
        isWatered = true;
        DayMonthManager.Instance.AddTime(2);
    }
    public override void AdvanceGrowth()
    {
        if (!isWatered)
        {
            return;
        }
        farmPlotState.tile.AdvanceGrowth();
        isWatered = false;
        if (farmPlotState.tile.IsFullyGrown())
        {
            farmPlotState.SwitchState(farmPlotState.HarvestStateInstance);
        }
    }
    private void OnEnable()
    {
        DayMonthManager.OnNewDay += AdvanceGrowth;
    }

    private void OnDisable()
    {
        DayMonthManager.OnNewDay -= AdvanceGrowth;
    }
}
