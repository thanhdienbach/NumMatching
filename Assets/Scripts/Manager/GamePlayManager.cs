using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{

    #region Instance
    public static GamePlayManager instance;
    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void OnDisable()
    {
        instance = null;
    }
    #endregion

    #region Variable
    [Header("Component")]
    [SerializeField] UIManager uIManager;
    [SerializeField] BoardManager boardManager;

    [Header("Matching number variable")]
    [SerializeField] Cell cell1;
    [SerializeField] Cell cell2;
    #endregion

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        uIManager = GetComponentInChildren<UIManager>();
        uIManager.Init();

        boardManager = uIManager.boardManager;
    }

    #region Input condition
    // Handle cell if have any click from cell
    public void OnClickEventHandle(ButtonClick _click)
    {
        AssignCells(_click);

        if (cell1 != null && cell2 != null)
        {
            CheckMatch(cell1, cell2);
        }
    }
    // Assign cell wich condition to cell1 or cell2 to handle
    void AssignCells(ButtonClick _click)
    {
        if (_click.numberCell == cell1)
        {
            cell1 = null;
        }
        else if (cell1 == null)
        {
            cell1 = _click.numberCell;
            cell1.buttonColor = Color.blue;
        }
        else if (cell2 == null && _click.numberCell != cell2)
        {
            cell2 = _click.numberCell;
        }
    }
    void ReAssignCells()
    {
        cell1 = cell2;
        cell2 = null;
    }
    #endregion

    #region match logic
    // Check cell1 and cell2 can matching or not.
    void CheckMatch(Cell _cell1, Cell _cell2)
    {
        if (RightValue(_cell1.value, _cell2.value))
        {
            Debug.Log("Check patch");
        }
        else
        {
            ReAssignCells();
        }
    }
    bool RightValue(int _value1, int _value2)
    {
        return _value1 == _value2 || _value1 + _value2 == boardManager.minValueOfCell + boardManager.maxValueOfCell;
    }
    #endregion
}
