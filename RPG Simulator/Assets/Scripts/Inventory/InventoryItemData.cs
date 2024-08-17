 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This inventory system was inspired by the online tutorial: https://www.youtube.com/watch?v=svoXugGLFwU&t=42s
[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject {
    public int ID;
    public string DisplayName;
    [TextArea(4,4)]
    public string Description;
    public Sprite Icon;
    public int MaxStackSize;
    
}
