using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public enum Mode
{
    Classic,
    Gem
}

public class ModePanle : MonoBehaviour
{
    [SerializeField] Button classicModeButton;
    [SerializeField] Button gemModeNutton;
    [SerializeField] GamePlayManager gamePlayManager;
    [SerializeField] UIManager uiManager;

    public void Init()
    {
        gamePlayManager = GamePlayManager.instance;
        uiManager = GetComponentInParent<UIManager>();
    }

    public void InitClassicMode()
    {
        gamePlayManager.mode = Mode.Classic;

        uiManager.playingPanle.Init(gamePlayManager.mode);
        uiManager.boardManager.Init(gamePlayManager.mode);
        this.gameObject.SetActive(false);
    }

    public void ChooseGemMode()
    {
        gamePlayManager.mode = Mode.Gem;

        uiManager.playingPanle.Init(gamePlayManager.mode);
        uiManager.boardManager.Init(gamePlayManager.mode);

        this.gameObject.SetActive(false);
    }
}
