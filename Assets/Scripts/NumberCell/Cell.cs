using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Button button;
    public Color buttonColor;
    public TMP_Text text;
    public Vector2Int position;
    public int value;
    public bool isMatched;
    public bool isGemCell;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        buttonColor = button.GetComponent<Image>().color;
        text = button.GetComponentInChildren<TMP_Text>();
    }

    // Awake numbercell from emptycell (Add value and set interactable = true). Number cell have value and player can interrac affter awake
    public void AwakeCell(int _minValue, int _maxValue)
    {
        int randomvalue = Random.Range(_minValue, _maxValue + 1);

        value = randomvalue;
        text.text = randomvalue.ToString();

        button.interactable = true;
    }
}
