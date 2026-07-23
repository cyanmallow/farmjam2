using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Inventory/Seed")]
public class SeedData : ItemData
{
    [Header("Growth")]
    public float growthTime;
    public float witherTime;
    public int resilient;

    [Header("Stages")]
    public Sprite[] stageSprites;

    [Header("Yield if harvested on time")]
    public ProductData cropProduct;
    public int minYield;
    public int maxYield;

    [Header("Yield if not harvested on time")]
    public int minSeedYield;
    public int maxSeedYield;
}

