using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tile))]
public class InteractableFieldWithStateMachine : MonoBehaviour, IInteractable
{
    private FarmPlotState farmPlotState;
    [SerializeField] private ItemData item;
    public SeedData testRadish;
    private Tile currentTile;
    private Inventory playerInventory;

    private void Awake()
    {
        if (farmPlotState == null)
        {
            farmPlotState = GetComponent<FarmPlotState>();
            currentTile = GetComponent<Tile>();
            playerInventory = FindFirstObjectByType<Inventory>();
        }
    }
    public void OnClickAction()
    {
        PlayerMovement.Instance.WalkTo(farmPlotState.transform);
        if (farmPlotState._currentState is EmptyState)
        {
            Debug.Log("Empty state clicked!");
            farmPlotState.SwitchState(farmPlotState.GrowingStateInstance);
            Plant(testRadish, currentTile);
            DayMonthManager.Instance.AddTime(2);
        }
        else if (farmPlotState._currentState is GrowingState)
        {
            Debug.Log("Growing state clicked!");
            farmPlotState.SwitchState(farmPlotState.HarvestStateInstance);
            Harvest();
        }
        else if (farmPlotState._currentState is HarvestState)
        {
            Debug.Log("Harvest state clicked!");
            farmPlotState.SwitchState(farmPlotState.EmptyStateInstance);
        }
        Debug.Log(farmPlotState.GetInstanceID());
    }

    private void Plant(SeedData seed, Tile tile)
    {
        Debug.Log($"Planting {seed.name}");
        // You can add more logic to handle the planting process
        tile.StartGrowing(seed);
    }
    private void Harvest()
    {
        currentTile.Harvest(playerInventory);
    }

    private void OnDisable()
    {
    }
}
