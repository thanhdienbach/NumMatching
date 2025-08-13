using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GemType
{
    None = -1,
    Gem1 = 1,
    Gem2 = 2
}

public class Cell : MonoBehaviour
{
    public Button button;
    public TMP_Text text;
    public Vector2Int position;
    public int value;
    public bool isMatched = true;
    public bool canMatching;
    public bool isGemCell;
    public GemType gemType = GemType.None;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        text = button.GetComponentInChildren<TMP_Text>();
    }

    // Awake numbercell from emptycell (Add value and set interactable = true). Number cell have value and player can interrac affter awake
    public void AwakeCell(int _value)
    {
        value = _value;
        text.text = value.ToString();

        button.interactable = true;

        isMatched = false;
    }

    public void ReAwakeCell()
    {
        value = 0;
        text.text = "";

        button.interactable = false;

        isMatched = true;
    }

    public void FrezeeCell()
    {
        button.GetComponent<Image>().color = Color.white;
        button.interactable = false;
        isMatched = true;
    }
}
