using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<string> selectedCardNames = new List<string>(); // Record selected cards

    [SerializeField] private Image avatarImage;
    public Sprite[] avatarSprites;

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
}
