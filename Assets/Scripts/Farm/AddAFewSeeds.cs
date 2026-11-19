using UnityEngine;

public class AddAFewSeeds : MonoBehaviour
{
    [SerializeField] private SeedData radishSeed;
    [SerializeField] private SeedData cornSeed;
    void Start()
    {
        Inventory.Instance.AddItem(radishSeed, 5);
        Inventory.Instance.AddItem(cornSeed, 5);
    }
}
