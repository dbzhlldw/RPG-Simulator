using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;
    private int inventorySize;
    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;
    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            InventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {

        // Check if the type of item exists in the inventory
        // If yes, add item to the item stack
        if (ContainsItem(itemToAdd, out List<InventorySlot> existSlot))
        {
            foreach (var slot in existSlot)
            {
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }

        // If not, add to the first available slot
        if (HasFreeSlot(out InventorySlot freeSlot)) 
        {
            freeSlot.UpdateSlot(itemToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }

        //inventorySlots[0] = new InventorySlot(itemToAdd, amountToAdd);

        // If the inventory is full, do not add the item
        return false;
    }

    public bool RemoveFromInventory(InventoryItemData itemToAdd, int amountToRemove)
    {

        // Check if the type of item exists in the inventory
        // If yes, add item to the item stack
        if (ContainsItem(itemToAdd, out List<InventorySlot> existSlot))
        {
            foreach (var slot in existSlot)
            {
                
                slot.RemoveFromStack(amountToRemove);
                OnInventorySlotChanged?.Invoke(slot);
                return true;
            }
        }

        return false;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> existSlot)
    {
        existSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();

        if (existSlot == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);

        if (freeSlot == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}