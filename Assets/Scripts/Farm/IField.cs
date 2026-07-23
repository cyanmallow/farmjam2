using UnityEngine;

[RequireComponent(typeof(Tile))]
public class IField : MonoBehaviour, IInteractable
{
    public FarmPlotState farmPlotState;
    public SeedData testRadish;
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
    public void Plant()
    {
        farmPlotState.CurrentState.Plant(testRadish);
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
