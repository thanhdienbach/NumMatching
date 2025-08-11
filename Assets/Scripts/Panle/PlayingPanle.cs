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

    public Button gem1;
    public TMP_Text textOfGem1;
    public Button gem2;
    public TMP_Text textOfGem2;

    public Button homeButton;

    public void Init(Mode _mode)
    {
        gameObject.SetActive(true);
        SetAddNumbersNumberText(GamePlayManager.instance.addNumbersNumber);
        SetAddStateText(GamePlayManager.instance.state);

        if (_mode == Mode.Gem)
        {
            gem1.gameObject.SetActive(true);
            textOfGem1.text = GamePlayManager.instance.numberOfGem1NeedToCollect.ToString();
            gem2.gameObject.SetActive(true);
            textOfGem2.text = GamePlayManager.instance.numberOfGem2NeedToCollect.ToString();
        }
    }

    public void SetAddNumbersNumberText(int _value)
    {
        addNumbersNumberText.text = _value.ToString();
    }

    public void SetAddStateText(int _state)
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

        for (int i = 0; i < GamePlayManager.instance.uIManager.boardManager.cells.Count; i++)
        {
            Destroy(GamePlayManager.instance.uIManager.boardManager.cells[i].gameObject);
        }
        GamePlayManager.instance.uIManager.boardManager.cells.Clear();
        GamePlayManager.instance.uIManager.boardManager.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
