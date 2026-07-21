using System;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

[RequireComponent(typeof(Tile))]
public class IField : MonoBehaviour, IInteractable
{
    public FarmPlotState farmPlotState;
    public SeedData testRadish;
    private Tile currentTile;
    private UIGrowCrops uIGrowCrops;
    private bool isWatered = false;

    private void Awake()
    {
        if (farmPlotState == null)
        {
            farmPlotState = GetComponent<FarmPlotState>();
        }
        currentTile = GetComponent<Tile>();
        uIGrowCrops = FindFirstObjectByType<UIGrowCrops>();
    }
    public void OnClickAction()
    {
        uIGrowCrops.ShowPanel(farmPlotState.transform);
        PlayerMovement.Instance.WalkTo(farmPlotState.transform);
        Debug.Log(farmPlotState.GetInstanceID());
    }

    public void Plant()
    {
        Debug.Log($"Planting {testRadish.name}");
        // You can add more logic to handle the planting process
        currentTile.StartGrowing(testRadish);
        farmPlotState.SwitchState(farmPlotState.GrowingStateInstance);
        DayMonthManager.Instance.AddTime(2);

    }
    public void Harvest()
    {
        currentTile.Harvest();
        farmPlotState.SwitchState(farmPlotState.EmptyStateInstance);
        DayMonthManager.Instance.AddTime(1);

    }
    public void Water()
    {
        currentTile.Water();
        DayMonthManager.Instance.AddTime(2);
        isWatered = true;
    }

    public void AdvanceGrowthAfterNewDay()
    {
        if (farmPlotState.GetCurrentFieldState() == farmPlotState.GrowingStateInstance && isWatered)
        {
            currentTile.AdvanceGrowth();
            isWatered = false;
            Debug.Log("A crop is watered and now growing!");
        }
    }
    private void OnEnable()
    {
        DayMonthManager.OnNewDay += AdvanceGrowthAfterNewDay;
    }

    private void OnDisable()
    {
        DayMonthManager.OnNewDay -= AdvanceGrowthAfterNewDay;
        uIGrowCrops.HidePanel();
    }
}
