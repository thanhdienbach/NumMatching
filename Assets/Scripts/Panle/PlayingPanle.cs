using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayingPanle : MonoBehaviour
{

    [Header("Enement")]
    public TMP_Text addNumbersNumberText;
    public Button addNumbersButton;

    public TMP_Text stateText;

    public Image winImage;
    public Image loseImage;

    public Button gem1;
    public TMP_Text textOfGem1;
    public Button gem2;
    public TMP_Text textOfGem2;

    public Button homeButton;

    public void Init()
    {
        gameObject.SetActive(true);

        if (GamePlayManager.instance.mode == Mode.Gem)
        {
            gem1.gameObject.SetActive(true);
            textOfGem1.text = GamePlayManager.instance.currentNumberOfGem1NeedToCollect.ToString();
            gem2.gameObject.SetActive(true);
            textOfGem2.text = GamePlayManager.instance.currentNumberOfGem2NeedToCollect.ToString();
        }
    }

    public void SetAddNumbersNumberText(int _value)
    {
        addNumbersNumberText.text = _value.ToString();
    }

    public void SetStateText(int _state)
    {
        stateText.text = "State " + _state.ToString();
    }

    public void SetGem1Text(int _value)
    {
        textOfGem1.text = _value.ToString();
    }
    public void SetGem2Text(int _value)
    {
        textOfGem2.text = _value.ToString();
    }

    public void BackToModePanle()
    {
        GamePlayManager.instance.uIManager.modePanle.gameObject.SetActive(true);

        GamePlayManager.instance.boardManager.ClearBoardData();
        GamePlayManager.instance.uIManager.boardManager.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void SetWinImageActiveToFalse()
    {
        winImage.gameObject.SetActive(false);
    }
    public void SetLoseImageActiveToFalse()
    {
        loseImage.gameObject.SetActive(false);
    }
}
