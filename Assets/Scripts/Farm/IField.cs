using UnityEngine;

[RequireComponent(typeof(Tile))]
public class IField : MonoBehaviour, IInteractable
{
    public FarmPlotState farmPlotState;
    public bool CanPlant => farmPlotState.CurrentState.CanPlant;
    public bool CanWater => farmPlotState.CurrentState.CanWater;
    public bool CanClear => farmPlotState.CurrentState.CanClear;
    public bool CanHarvest => farmPlotState.CurrentState.CanHarvest;
    public string StateName => farmPlotState.CurrentState.StateName;
    private void Awake()
    {
        farmPlotState = GetComponent<FarmPlotState>();
    }
    public void OnClickAction()
    {
        OptionsFarmPlot.Instance.DoWhatWithPlot(this);
        PlayerMovement.Instance.WalkTo(farmPlotState.transform);
    }
    public void Plant(SeedData seed)
    {
        farmPlotState.CurrentState.Plant(seed);
    }
    public void Harvest()
    {
        farmPlotState.CurrentState.Harvest();
    }
    public void Water()
    {
        farmPlotState.CurrentState.Water();
    }
    public void AdvanceGrowth()
    {
        farmPlotState.CurrentState.GrowUp();
    }
    public void Clear()
    {
        farmPlotState.CurrentState.Clear();
    }
}
