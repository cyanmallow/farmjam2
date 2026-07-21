using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public Sprite itemIcon;
    [TextArea] public string itemDescription;
    public int maxStackSize = 99;
}

public enum ItemType
{
    Product,
    Seed,
    Other
}
