using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int slotCount = 20;
    public List<InventorySlot> slots;
    public event Action OnInventoryChanged;
    void Awake()
    {
        slots = new List<InventorySlot>();
        for (int i = 0; i < slotCount; i++)
        {
            slots.Add(new InventorySlot());
        }
    }

    public bool AddItem(ItemData item, int quantity = 1)
    {
        // todo: check if item is stackable and if so, add to existing stack first
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty)
            {
                int spaceLeft = item.maxStackSize - slot.quantity;
                int amountToAdd = Mathf.Min(spaceLeft, quantity);
                slot.quantity += amountToAdd;
                quantity -= amountToAdd;
                if (quantity <= 0)
                {
                    OnInventoryChanged?.Invoke();
                    Debug.Log($"Added {amountToAdd} of {item.itemName} to existing stack. Remaining quantity: {quantity}");
                    return true;

                }
            }
            else if (slot.IsEmpty)
            {
                slot.item = item;
                slot.quantity = quantity;
                quantity = 0;
                OnInventoryChanged?.Invoke();
                Debug.Log($"Added {quantity} of {item.itemName} to empty slot. Remaining quantity: {quantity}");
                return true;
            }
        }
        OnInventoryChanged?.Invoke();
        Debug.Log($"Could not add {quantity} of {item.itemName}. Inventory full.");
        return quantity <= 0; // Inventory full
    }

    public bool RemoveItem(ItemData item, int quantity = 1)
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
