using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [Header("Component")]
    public UIManager uIManager;


    private void Start()
    {
        Init();
    }
    public void Init()
    {
        uIManager = GetComponentInChildren<UIManager>();
        uIManager.Init();
    }
}
