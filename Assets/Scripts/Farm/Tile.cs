using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class Tile : MonoBehaviour
{
    private SeedData plantedSeed;
    private float growthTime;
    public bool IsFullyGrown { get; set; }
    private float witherTime;
    public int deadPoint;

    public void StartGrowing(SeedData seed)
    {
        plantedSeed = seed;
        growthTime = seed.growthTime;
        witherTime = 0f;
        deadPoint = seed.resilient;
        Debug.Log($"Started growing {seed.name} with growth time of {growthTime} days.");
    }

    public void HarvestWhilePlantNotDead()
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
    public void HarvestWhilePlantDying()
    {
        if (IsFullyGrown)
        {
            Inventory.Instance.AddItem(plantedSeed.cropProduct, Random.Range(plantedSeed.minYield, plantedSeed.maxYield)/2);
            Inventory.Instance.AddItem(plantedSeed, Random.Range(plantedSeed.minSeedYield, plantedSeed.maxSeedYield)/2);
            Debug.Log("Now inventory: " + Inventory.Instance.GetItemCount(plantedSeed.cropProduct) + " " + plantedSeed.cropProduct + " and " + Inventory.Instance.GetItemCount(plantedSeed) + " " + plantedSeed);
            plantedSeed = null;
        }
        else
        {
            Debug.LogWarning("Cannot harvest. The plant is not fully grown.");
        }
    }
    public void HarvestWhilePlantAlreadyDead()
    {
        if (plantedSeed != null)
        {
            Inventory.Instance.AddItem(plantedSeed, Random.Range(plantedSeed.minSeedYield, plantedSeed.maxSeedYield));
            Debug.Log($"Harvested the dead crop and added {plantedSeed} to the inventory.");
            Debug.Log("Now inventory: " + Inventory.Instance.GetItemCount(plantedSeed) + " " + plantedSeed);
            plantedSeed = null;
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
        else
        {
            Debug.Log($"The plant is still growing. {growthTime} days left until fully grown.");
        }
    }

    public bool CheckIfPlantIsDead()
    {
        if (deadPoint <= 0)
        {
            Debug.LogWarning("The plant has died.");
            return true;
        }
        return false;
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
