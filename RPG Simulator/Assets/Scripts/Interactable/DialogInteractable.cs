using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DialogInteractable : Interactable, IInteractable
{
    public Flowchart dialog;

    public void Interact(PlayerController player)
    {
        if (dialog != null)
        {
            dialog.ExecuteBlock("New Block");
        }
    }
}
