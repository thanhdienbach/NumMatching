using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardManager : MonoBehaviour
{
    [Header("Game object")]
    [SerializeField] Cell cellPrefabs;

    [Header("Board size")]
    [SerializeField] int colums = 9;
    [SerializeField] int rows = 12;
    [SerializeField] int maxCellFilled = 27;
    [SerializeField] Transform contentRectTransform;

    [Header("Board handle Cell")]
    [SerializeField] Cell[] cells;
    public int minValueOfCell = 1;
    public int maxValueOfCell = 9;

    public void Init()
    {
        GenerateEmptyCell();
    }

    /// <summary>
    /// Generater empty numbercell (numbercell don't have value, can't interac) and set parent to board
    /// </summary>
    public void GenerateEmptyCell()
    {
        for (int i = 0; i < colums * rows; i++)
        {
            Cell cell = Instantiate(cellPrefabs, contentRectTransform);
            cell.position = SetPositionNumberCell(i);

            if (i < maxCellFilled)
            {
                cell.AwakeCell(minValueOfCell, maxValueOfCell);
            }
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
    
}
