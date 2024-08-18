using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<string> selectedCardNames = new List<string>(); // Record selected cards

    [SerializeField] private Image avatarImage;
    public Sprite[] avatarSprites;

    private int mushroomsCollected = 0;
    [SerializeField] private Image mushroomIcon;
    [SerializeField] private Text mushroomCountText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RecordCardSelection(string cardName)
    {
        selectedCardNames.Add(cardName);
        Debug.Log("Card selected: " + cardName);

        if (selectedCardNames.Count == 1)
        {
            UpdateAvatarImage(cardName);
        }
    }

    public List<string> GetSelectedCardNames()
    {
        return selectedCardNames;
    }

    private void UpdateAvatarImage(string cardName)
    {
        string avatarName = "Avatar_" + cardName.Substring(2); // e.g. "C_Chosen" -> "Avatar_Chosen"
        Sprite newSprite = FindSpriteByName(avatarName);
        if (newSprite != null)
        {
            avatarImage.sprite = newSprite;
        }
        else
        {
            Debug.LogError("Sprite not found: " + avatarName);
        }
    }

    private Sprite FindSpriteByName(string name)
    {
        foreach (Sprite sprite in avatarSprites)
        {
            if (sprite.name == name)
            {
                return sprite;
            }
        }
        return null;
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void RecordMushroomCollection()
    {
        mushroomsCollected++; // Increment the count of mushrooms collected
        UpdateMushroomUI();
        Debug.Log("Mushroom collected. Total mushrooms: " + mushroomsCollected);

        // Add additional logic here if needed, such as updating UI or triggering events
    }

    // Optional: A method to get the current count of mushrooms collected
    public int GetMushroomsCollected()
    {
        return mushroomsCollected;
    }

    private void UpdateMushroomUI()
    {
        if (mushroomsCollected > 0)
        {
            mushroomIcon.gameObject.SetActive(true);  // Make sure the mushroom icon is visible
            mushroomCountText.text = mushroomsCollected.ToString();  // Update the text to the current count
        }
        else
        {
            mushroomIcon.gameObject.SetActive(false);  // Hide the icon if there are no mushrooms collected
        }
    }
}
