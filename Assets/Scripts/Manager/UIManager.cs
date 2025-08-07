using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Component")]
    public BoardManager boardManager;

    public void Init()
    {
        boardManager = GetComponentInChildren<BoardManager>();
        boardManager.Init();
    }
}
