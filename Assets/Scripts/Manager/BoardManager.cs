using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardManager : MonoBehaviour
{
    [Header("Game object")]
    [SerializeField] NumberCell numberCellPrefabs;

    [Header("Board size")]
    [SerializeField] int colums = 9;
    [SerializeField] int rows = 12;
    [SerializeField] int maxCellFilled = 27;
    [SerializeField] Transform contentRectTransform;

    [Header("Board handle Cell")]
    [SerializeField] NumberCell[] numberCells;
    [SerializeField] int minValueOfNumberCell = 1;
    [SerializeField] int maxValueOfNumberCell = 9;

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
            NumberCell numberCell = Instantiate(numberCellPrefabs, contentRectTransform);
            numberCell.position = SetPositionNumberCell(i);

            if (i < maxCellFilled)
            {
                numberCell.AwakeCell(minValueOfNumberCell, maxValueOfNumberCell);
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
