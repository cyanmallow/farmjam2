using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int slotCount = 20;
    public List<InventorySlot> slots;
    public event Action OnInventoryChanged;
    public static Inventory Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        slots = new List<InventorySlot>();
        for (int i = 0; i < slotCount; i++)
        {
            slots.Add(new InventorySlot());
        }
    }
    public bool AddItem(ItemData item, int amountToAdd)
    {
        // todo: check if item exists and if so, add to existing stack first
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty)
            {
                slot.quantity += amountToAdd;
            }
            else if (slot.IsEmpty)
            {
                slot.item = item;
                slot.quantity = amountToAdd;
                OnInventoryChanged?.Invoke();
            }
            Debug.Log($"Added {amountToAdd} of {item.itemName} to empty slot. Remaining quantity: {slot.quantity}");
        }
        OnInventoryChanged?.Invoke();
        return true; // Successfully added the item to the inventory
    }
    public bool RemoveItem(ItemData item, int quantity)
    {
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty && slot.item == item)
            {
                if (slot.quantity >= quantity)
                {
                    slot.quantity -= quantity;
                    if (slot.quantity == 0)
                    {
                        slot.item = null;
                    }
                    OnInventoryChanged?.Invoke();
                    return true;
                }
                else
                {
                    quantity -= slot.quantity;
                    slot.quantity = 0;
                    slot.item = null;
                }
            }
        }
        OnInventoryChanged?.Invoke();
        return false; // Not enough items to remove
    }
    public int GetItemCount(ItemData item)
    {
        int count = 0;
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty && slot.item == item)
            {
                count += slot.quantity;
            }
        }
        return count;
    }
}
