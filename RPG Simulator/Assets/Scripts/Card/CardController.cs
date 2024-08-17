using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardController : MonoBehaviour
{
    public Image frontImage;
    public Sprite[] cardFaces;
    public Button cardButton;

    private Sprite currentSprite;

    void Start()
    {
        cardButton.onClick.AddListener(ShowCard);
        SetButtonColorNormal(); // This button
    }

    void ShowCard()
    {
        int randomIndex = Random.Range(0, cardFaces.Length);
        currentSprite = cardFaces[randomIndex];
        frontImage.sprite = currentSprite;
        frontImage.gameObject.SetActive(true);

        SetButtonColorSelected(); // This button

        GameManager.Instance.RecordCardSelection(currentSprite.name); // Call GameManager to record the card

        DisableOtherCards();

        StartSelectedCardAnimation();

        GetComponent<RectTransform>().SetAsLastSibling(); // Ensure selected card is on top of other cards
    }

    void StartSelectedCardAnimation()
    {
        RectTransform cardRect = GetComponent<RectTransform>();

        cardRect.anchorMin = new Vector2(0.5f, 0.5f);
        cardRect.anchorMax = new Vector2(0.5f, 0.5f);
        cardRect.pivot = new Vector2(0.5f, 0.5f);

        Sequence mySequence = DOTween.Sequence();
        mySequence.AppendInterval(0.3f);
        mySequence.Append(transform.DOScale(new Vector3(1.8f, 1.8f, 1), 1.5f)); // Scale Up
        mySequence.Join(cardRect.DOAnchorPos(Vector2.zero, 1.5f)); // Move to Canvas center
    }

    private void SetButtonColorNormal()
    {
        ColorBlock colors = cardButton.colors;
        colors.disabledColor = colors.normalColor;  // Not change colour when button disabled
        cardButton.colors = colors;
    }

    private void SetButtonColorSelected()
    {
        ColorBlock colors = cardButton.colors;
        colors.disabledColor = colors.normalColor;
        cardButton.colors = colors;
    }

    void DisableOtherCards()
    {
        CardController[] allCards = FindObjectsOfType<CardController>();
        foreach (CardController card in allCards)
        {
            if (card != this)
            {
                card.cardButton.interactable = false;
                card.SetButtonColorDisabled();
            }
        }
    }

    private void SetButtonColorDisabled()
    {
        ColorBlock colors = cardButton.colors;
        colors.disabledColor = new Color(colors.normalColor.r, colors.normalColor.g, colors.normalColor.b, 0.5f); // 设置禁用状态下为半透明
        cardButton.colors = colors;
    }

    public string GetCardName()
    {
        if (currentSprite != null)
        {
            return currentSprite.name;
        }
        return "No sprite set";
    }
}