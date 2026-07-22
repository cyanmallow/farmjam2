using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class Tile : MonoBehaviour
{
    private SeedData plantedSeed;
    private float growthTime;
    private bool IsFullyGrown { get; set; }
    private float witherTime;
    private bool isWatered;


    public void StartGrowing(SeedData seed)
    {
        plantedSeed = seed;
        growthTime = seed.growthTime;
        witherTime = 0f;
        Debug.Log($"Started growing {seed.name} with growth time of {growthTime} seconds.");
        AdvanceGrowth();
    }

    public void Water()
    {
        // if current state = growing state
        if (plantedSeed != null && !IsFullyGrown)
        {
            isWatered = true;
            DayMonthManager.Instance.AddTime(2);
        }
    }

    public void Harvest()
    {
        if (IsFullyGrown)
        {
            Inventory.Instance.AddItem(plantedSeed.cropProduct, Random.Range(plantedSeed.minYield, plantedSeed.maxYield));
            Debug.Log($"Harvested the crop and added {plantedSeed.cropProduct} to the inventory.");
            Debug.Log("Now inventory: " + Inventory.Instance.GetItemCount(plantedSeed.cropProduct) + " " + plantedSeed.cropProduct);
            plantedSeed = null;
        }
        else
        {
            Debug.LogWarning("Cannot harvest. The plant is not fully grown.");
        }
    }

    public void Wither()
    {
        if (plantedSeed != null && !IsFullyGrown)
        {
            Inventory.Instance.AddItem(plantedSeed, Random.Range(plantedSeed.minSeedYield, plantedSeed.maxSeedYield));
            plantedSeed = null;
        }
        else
        {
            Debug.LogWarning("Cannot wither. The plant is either fully grown or not planted.");
        }
    }

    //start countdown for growth time and wither time
    // new day growthTime -= 1f;
    public void AdvanceGrowth()
    {
        growthTime -= 1f;
        if (growthTime <= 0f)
        {
            IsFullyGrown = true;
            witherTime = plantedSeed.witherTime;
            Debug.Log($"The plant has fully grown. It will wither in {witherTime} days.");

        }

    }
    private void StartWitherCountdown()
    {
        if (IsFullyGrown)
        {
            growthTime -= 1f;
            if (growthTime <= 0f)
            {
                IsFullyGrown = false;
                witherTime = plantedSeed.witherTime;
                Debug.Log($"The plant has fully grown. It will wither in {witherTime} days.");
            }
        }
        else
        {
            witherTime -= 1f;
            if (witherTime <= 0f)
            {
                Debug.LogWarning("The plant has withered.");
                plantedSeed = null;
            }
        }
    }
}
