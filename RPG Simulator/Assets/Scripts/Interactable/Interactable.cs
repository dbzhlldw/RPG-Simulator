using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool isPlayerCollided = false;
    private PlayerController playerCollided;
    private GameObject interactionIcon;

    void Update()
    {
        if (isPlayerCollided && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            isPlayerCollided = true;
            playerCollided = player;
            SetInteractionAttention(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            SetInteractionAttention(false);
            isPlayerCollided = false;
            playerCollided = null;
        }
    }

    private GameObject GetIconByName(string iconName)
    {
        if (playerCollided != null)
        {
            Transform iconTransform = playerCollided.transform.Find(iconName);
            if (iconTransform != null)
            {
                return iconTransform.gameObject;
            }
        }
        return null;
    }

    public void SetInteractionAttention(bool isActive)
    {
        string iconName = "";
        if (CompareTag("Search") || CompareTag("Item"))
        {
            iconName = "SearchIcon";
            ActivateChildWithTag("Highlight", isActive);
        }
        else if (CompareTag("Door"))
        {
            iconName = "DoorIcon";
        }
        else if (CompareTag("NPC"))
        {
            iconName = "TalkIcon";
        }

        GameObject icon = GetIconByName(iconName);
        if (icon != null)
        {
            icon.SetActive(isActive);
        }
    }

    private void ActivateChildWithTag(string tag, bool isActive)
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag(tag))
            {
                child.gameObject.SetActive(isActive);
                return;
            }
        }
    }

    protected void Interact()
    {
        if (playerCollided != null)
        {
            (this as IInteractable)?.Interact(playerCollided);
        }
        else
        {
            // Player not collided, try to find the player GameObject
            GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
            if (playerGameObject != null)
            {
                playerCollided = playerGameObject.GetComponent<PlayerController>();
                if (playerCollided != null)
                {
                    (this as IInteractable)?.Interact(playerCollided);
                }
            }
        }
    }

    public void TriggerInteraction()
    {
        Interact();
    }
}