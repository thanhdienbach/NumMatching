using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardManager : MonoBehaviour
{
    [Header("Game object")]
    [SerializeField] Cell cellPrefabs;

    [Header("Board size")]
    public int colums = 9;
    [SerializeField] int rows = 12;
    [SerializeField] int maxCellFilled = 27;
    [SerializeField] Transform contentRectTransform;

    [Header("Board handle Cell")]
    public List<Cell> cells;
    public int minValueOfCell = 1;
    public int maxValueOfCell = 9;

    public void Init()
    {
        GenerateEmptyCell(colums * rows);
        GenerateValue();
    }

    /// <summary>
    /// Generater empty numbercell (numbercell don't have value, can't interac) and set parent to board
    /// </summary>
    public void GenerateEmptyCell(int _numberOfCell)
    {
        for (int i = 0; i < _numberOfCell; i++)
        {
            Cell cell = Instantiate(cellPrefabs, contentRectTransform);
            cell.position = SetPositionNumberCell(cells.Count);

            cells.Add(cell);
        }
    }
    void GenerateValue()
    {
        for (int i = 0; i < maxCellFilled; i++)
        {
            cells[i].AwakeCell(minValueOfCell, maxValueOfCell);
        }
        
    }
    // Caculate position of numbercell by index of it in array
    Vector2Int SetPositionNumberCell(int _index)
    {
        int positionX = _index / colums + 1;
        int positionY = _index % colums + 1;
        Vector2Int position = new Vector2Int(positionX, positionY);
        return position;
    }
    
    public void ClearRowHandle(int _row)
    {
        int startIndex = (_row - 1) * colums;
        if (cells.Count > colums * rows - 1)
        {
            GenerateEmptyCell(colums);
        }
        for (int i = startIndex; i < startIndex + colums; i++)
        {
            Destroy(cells[i].gameObject);
        }
        for (int i = startIndex; i < startIndex + colums; i++)
        {
            cells.Remove(cells[startIndex]);
        }
        for (int i = startIndex; i < cells.Count; i++)
        {
            cells[i].position.x -= 1;
        }
    }
}
