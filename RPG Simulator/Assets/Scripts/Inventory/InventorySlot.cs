using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot 
{
    [SerializeField] private InventoryItemData itemData;
    [SerializeField] private int stackSize;
    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;
    public InventorySlot(InventoryItemData source, int amount)
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemData = null;
        stackSize = -1;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    // If there already exists the picked item type, update the item to an existing slot
    public void UpdateSlot(InventoryItemData data, int amount)
    {
        itemData = data;
        stackSize = amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
        if (stackSize == 0) 
        {
            ClearSlot();
        }
    }

    public bool RoomLeftInStack(int addAmount)
    {
        if (stackSize + addAmount <= itemData.MaxStackSize) return true;
        else return false;
    }
}
