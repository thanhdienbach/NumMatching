using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] GamePlayManager gamePlayManager;
    public Cell numberCell;

    private void OnEnable()
    {
        gamePlayManager = GamePlayManager.instance;
    }

    public void OnClick()
    {
        gamePlayManager.OnClickEventHandle(this);
    }
}
