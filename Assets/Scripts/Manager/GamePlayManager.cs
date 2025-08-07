using System;
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
    [SerializeField] AudioManager audioManager;
    [SerializeField] UIManager uIManager;
    [SerializeField] BoardManager boardManager;

    [Header("Matching number variable")]
    [SerializeField] Cell cell1;
    [SerializeField] Cell cell2;
    [SerializeField] Vector2[] direction = new Vector2[]
    {
        new Vector2(-1, -1),
        new Vector2(-1, 0),
        new Vector2(-1, 1),
        new Vector2(0, -1),
        new Vector2(0, 1),
        new Vector2(1, -1),
        new Vector2(1, 0),
        new Vector2(1, 1),
        
    };
    #endregion

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        audioManager = GetComponentInChildren<AudioManager>();
        audioManager.Init();
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
            if(IsMatching(cell1, cell2))
            {
                int rowCanclear1 = cell1.position.x;
                int rowCanclear2 = cell2.position.x;

                HandleMatchingCells(cell1, cell2);

                UpdateBoard(rowCanclear1, rowCanclear2);
            }
            
        }
    }

    // Assign cell wich condition to cell1 or cell2 to handle
    void AssignCells(ButtonClick _click)
    {
        if (_click.numberCell == cell1)
        {
            cell1.button.GetComponent<Image>().color = Color.white;
            cell1 = null;
        }
        else if (cell1 == null)
        {
            cell1 = _click.numberCell;
            cell1.button.GetComponent<Image>().color = Color.green;
        }
        else if (cell2 == null && _click.numberCell != cell2)
        {
            cell2 = _click.numberCell;
            cell2.button.GetComponent<Image>().color = Color.green;
        }
    }
    void ReAssignCells()
    {
        cell1.button.GetComponent<Image>().color = Color.white;
        cell1 = cell2;
        cell2.button.GetComponent<Image>().color = Color.green;
        cell2 = null;
    }
    void Deselect(Cell _cell)
    {
        _cell.button.GetComponent<Image>().color = Color.white;
    }
    #endregion

    #region Match logic
    // Check cell1 and cell2 can matching or not.
    bool IsMatching(Cell _cell1, Cell _cell2)
    {
        if (IsRightValue(_cell1.value, _cell2.value))
        {
            if (IsClearPath(_cell1, _cell2))
            {
                return true;
            }
            else
            {
                Deselect(_cell1);
                Deselect(_cell2);
                cell1 = null;
                cell2 = null;
            }
        }
        else
        {
            ReAssignCells();
        }
        return false;
    }
    bool IsRightValue(int _value1, int _value2)
    {
        return _value1 == _value2 || _value1 + _value2 == boardManager.minValueOfCell + boardManager.maxValueOfCell;
    }
    bool IsClearPath(Cell _cell1, Cell _cell2)
    {
        int dx = Math.Sign(_cell2.position.x - _cell1.position.x);
        int dy = Math.Sign(_cell2.position.y - _cell1.position.y);
        Vector2Int direction = new Vector2Int(dx, dy);
        
        Vector2Int nextPosition = _cell1.position + direction;
        Cell nextCell = NextCell(nextPosition);

        for (int i = 0; i < boardManager.colums; i++)
        {
            if (nextCell == null)
            {
                return false;
            }
            if (!nextCell.isMatched && nextCell != _cell2)
            {
                return false;
            }
            if (nextCell.position == _cell2.position)
            {
                return true;
            }
            nextPosition += direction;
            nextCell = NextCell(nextPosition);
        }
        return false;
    }
    Cell NextCell(Vector2Int _position)
    {
        int startIndexOfCell = ((_position.x - 1) * boardManager.colums);
        for (int i = startIndexOfCell; i < startIndexOfCell + boardManager.colums; i++)
        {
            if (boardManager.cells[i].position == _position)
            {
                return boardManager.cells[i];
            }
        }
        return null;
    }
    #endregion

    #region Handle affter cells matching
    void HandleMatchingCells(Cell _cell1, Cell _cell2)
    {
        _cell1.FrezeeCell();
        _cell2.FrezeeCell();
        cell1 = null;
        cell2 = null;
    }
    void UpdateBoard(int _row1, int _row2)
    {
        if (_row2 < _row1)
        {
            int temp = _row1;
            _row1 = _row2;
            _row2 = temp;
        }
        if (_row1 == _row2)
        {
            if (IsClearedRow(_row1))
            {
                boardManager.ClearRowHandle(_row1);
            }
        }
        else if (_row1 != _row2)
        {
            if (IsClearedRow(_row1))
            {
                boardManager.ClearRowHandle(_row1);
            }
            if (IsClearedRow(_row2 - 1))
            {
                boardManager.ClearRowHandle(_row2 - 1);
            }
        }
    }
    bool IsClearedRow(int _row)
    {
        int countMatchedCell = 0;
        int startIndex = (_row - 1) * boardManager.colums;
        for (int i = startIndex; i < startIndex + boardManager.colums; i++)
        {
            if (boardManager.cells[i].isMatched)
            {
                countMatchedCell++;
            }
        }
        return countMatchedCell == boardManager.colums;
    }
    #endregion
}
