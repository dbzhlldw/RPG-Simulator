using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class InteractableItem : Interactable, IInteractable
{
    // List to hold multiple items
    public List<ItemData> items;

    public Flowchart dialog;

    void Start() {
        
    }

    public void Interact(PlayerController player)
    {
        if (dialog != null)
        {
            dialog.ExecuteBlock("New Block");
        }
    }
}

// Define a class to store the item data
[System.Serializable]
public class ItemData
{
    public string itemName;
    public string description;
    public string spritePath;
}