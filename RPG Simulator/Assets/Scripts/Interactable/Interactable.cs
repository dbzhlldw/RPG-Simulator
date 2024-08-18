using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool isPlayerNearby = false;  // Flag to check if the player is nearby
    private GameObject player;  // Reference to the player GameObject

    void Update()
    {
        // Check if the player is nearby and presses "E" to interact
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            CollectMushroom();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            player = other.gameObject;  // Store reference to the player
            SetInteractionAttention(true);  // Show interaction icon
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            SetInteractionAttention(false);  // Hide interaction icon
            player = null;  // Clear reference to the player
        }
    }

    private void CollectMushroom()
    {
        Debug.Log("Mushroom collected.");
        GameManager.Instance.RecordMushroomCollection();
        gameObject.SetActive(false);  // Hide or destroy the mushroom
    }

    private GameObject GetIconByName(string iconName)
    {
        if (player != null)
        {
            Transform iconTransform = player.transform.Find(iconName);
            if (iconTransform != null)
            {
                return iconTransform.gameObject;
            }
        }
        return null;
    }

    public void SetInteractionAttention(bool isActive)
    {
        string iconName = "SearchIcon"; 
        GameObject icon = GetIconByName(iconName);
        if (icon != null)
        {
            icon.SetActive(isActive);
        }
    }
}