using UnityEngine;

[RequireComponent(typeof(Tile))]
public class IField : MonoBehaviour, IInteractable
{
    public FarmPlotState farmPlotState;
    private UIGrowCrops uIGrowCrops;

    private void Awake()
    {
        farmPlotState = GetComponent<FarmPlotState>();
        uIGrowCrops = FindFirstObjectByType<UIGrowCrops>();
    }
    public void OnClickAction()
    {
        uIGrowCrops.ShowPanel(this);
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
}
