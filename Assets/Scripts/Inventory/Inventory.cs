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
        if (item == null || amountToAdd <= 0) return false;

        // 1. add to an existing stack of the same item
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty && slot.item == item)
            {
                slot.quantity += amountToAdd;
                OnInventoryChanged?.Invoke();
                return true;
            }
        }

        // 2. otherwise use the first empty slot
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.item = item;
                slot.quantity = amountToAdd;
                OnInventoryChanged?.Invoke();
                return true;
            }
        }

        Debug.LogWarning("Inventory full");
        return false;
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
