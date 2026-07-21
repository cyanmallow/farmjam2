using UnityEngine;

[CreateAssetMenu(fileName = "New Product", menuName = "Inventory/Product")]
public class ProductData : ItemData
{
    [Header("Fullness")]
    public float fullnessValue;
}

