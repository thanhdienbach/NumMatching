using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Component")]
    public BoardManager boardManager;
    public PlayingPanle playingPanle;


    public void Init()
    {
        boardManager = GetComponentInChildren<BoardManager>();
        boardManager.Init();

        playingPanle = GetComponentInChildren<PlayingPanle>();
        playingPanle.Init();
    }
}
