using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayingPanle : MonoBehaviour
{

    [Header("Enement")]
    public TMP_Text addNumbersNumberText;
    public Button addNumbersButton;

    public void Init()
    {
        SetAddNumbersNumberText(GamePlayManager.instance.addNumbersNumber);
    }

    public void SetAddNumbersNumberText(int _value)
    {
        addNumbersNumberText.text = _value.ToString();
    }
}
