using UnityEngine;

public class InventorySlot 
{
    public ItemData item;
    public int quantity;

    public bool IsEmpty => item == null || quantity <= 0;

    public InventorySlot() { }
    public InventorySlot(ItemData item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public void Clear()
    {
        item = null;
        quantity = 0;
    }
}
