using System.Runtime.CompilerServices;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SeedData plantedSeed;
    private float growthTime;
    private bool isFullyGrown;
    private float witherTime;

    public void StartGrowing(SeedData seed)
    {
        plantedSeed = seed;
        growthTime = seed.growthTime;
        isFullyGrown = true;
        witherTime = 0f;
        Debug.Log($"Started growing {seed.name} with growth time of {growthTime} seconds.");
    }

    public void Harvest(Inventory inventory)
    {
        if (isFullyGrown)
        {
            inventory.AddItem(plantedSeed.cropProduct, Random.Range(plantedSeed.minYield, plantedSeed.maxYield));
            Debug.Log($"Harvested the crop and added {plantedSeed.cropProduct} to the inventory.");
            Debug.Log("Now inventory: " + inventory.GetItemCount(plantedSeed.cropProduct) + " " + plantedSeed.cropProduct);
            plantedSeed = null;

        }
        else
        {
            Debug.LogWarning("Cannot harvest. The plant is not fully grown.");
        }
    }

    public void Wither(Inventory inventory)
    {
        if (plantedSeed != null && !isFullyGrown)
        {
            inventory.AddItem(plantedSeed, Random.Range(plantedSeed.minSeedYield, plantedSeed.maxSeedYield));
            plantedSeed = null;
        }
        else
        {
            Debug.LogWarning("Cannot wither. The plant is either fully grown or not planted.");
        }
    }
}
