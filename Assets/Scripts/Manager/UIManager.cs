using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Component")]
    public BoardManager boardManager;
    public PlayingPanle playingPanle;
    public ModePanle modePanle;


    public void Init()
    {
        boardManager = GetComponentInChildren<BoardManager>(true);

        playingPanle = GetComponentInChildren<PlayingPanle>(true);

        modePanle = GetComponentInChildren<ModePanle>(true);
        modePanle.Init();
    }
}
