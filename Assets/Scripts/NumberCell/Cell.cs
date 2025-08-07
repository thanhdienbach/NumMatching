using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Button button;
    public TMP_Text text;
    public Vector2Int position;
    public int value;
    public bool isMatched = true;
    public bool isGemCell;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        text = button.GetComponentInChildren<TMP_Text>();
    }

    // Awake numbercell from emptycell (Add value and set interactable = true). Number cell have value and player can interrac affter awake
    public void AwakeCell(int _minValue, int _maxValue)
    {
        int randomvalue = Random.Range(_minValue, 3);

        value = randomvalue;
        text.text = randomvalue.ToString();

        button.interactable = true;

        isMatched = false;
    }

    public void FrezeeCell()
    {
        button.GetComponent<Image>().color = Color.white;
        button.interactable = false;
        isMatched = true;
    }
}
