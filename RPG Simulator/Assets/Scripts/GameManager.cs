using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private CardController lastSelectedCard;

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

    public void SelectCard(CardController card)
    {
        if (lastSelectedCard != null)
        {
            
        }

        lastSelectedCard = card;
        Debug.Log("Selected card: " + card.GetCardName());
    }

    public string GetLastSelectedCardName()
    {
        if (lastSelectedCard != null)
        {
            return lastSelectedCard.GetCardName();
        }
        return "No card selected";
    }
}
