using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public Image ItemSprite;
    public TextMeshProUGUI ItemCount;

    void Awake()
    {
        ItemSprite.color = Color.clear;
        ItemCount.text = "";
    }
}
