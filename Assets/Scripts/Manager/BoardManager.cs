using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UIElements;


public class BoardManager : MonoBehaviour
{
    #region Variable
    [Header("Component")]
    [SerializeField] GamePlayManager gamePlayManager;

    [Header("Game object")]
    [SerializeField] Cell cellPrefabs;

    [Header("Board size")]
    public int colums = 9;
    [SerializeField] int rows = 12;
    public int startCellFilled = 42;
    [SerializeField] Transform contentRectTransform;

    [Header("Board handle Cell")]
    public List<Cell> cells;
    public int minValueOfCell = 1;
    public int maxValueOfCell = 9;
    public bool generatedEmptycell = false;

    [Header("Generate munber with condition variable")]
    [SerializeField] List<int> numbers = new List<int>();
    [SerializeField] List<int> baseNumbers1 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    [SerializeField] List<int> baseNumbers2 = new List<int> { 3, 4, 1, 2, 9, 8, 5, 6, 7 };
    [SerializeField] Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
        new Vector2Int(0, -1),
        new Vector2Int(0, 1),
        new Vector2Int(1, -1),
        new Vector2Int(1, 0),
        new Vector2Int(1, 1),

    };
    [SerializeField] bool assigned;
    [SerializeField] int countLoop = 0;

    [Header("Gem mode variable")]
    [SerializeField] int gemTileAppearanceRate;
    [SerializeField] int lastIndexOfCellWillAppearanceGem;
    [SerializeField] int maxCountOfGemsWhenAddNumbers;
    [SerializeField] List<Sprite> gemSprite;
    [SerializeField] List<int> gemType;
    #endregion

    public void Init(Mode _mode)
    {
        gameObject.SetActive(true);

        gamePlayManager = GamePlayManager.instance;

        if (gamePlayManager.mode == Mode.Gem)
        {
            gemType = new List<int>() { 1, 2, 1, 2, 1, 2, 1, 2 };
            gamePlayManager.currentNumberOfGem1NeedToCollect = 4;
            gamePlayManager.uIManager.playingPanle.SetGem1Text(gamePlayManager.currentNumberOfGem1NeedToCollect);
            gamePlayManager.currentNumberOfGem2NeedToCollect = 4;
            gamePlayManager.uIManager.playingPanle.SetGem2Text(gamePlayManager.currentNumberOfGem2NeedToCollect);
            gamePlayManager.countOfGemType = 2;
        }

        GenerateEmptyCell(colums * rows);

        GenerateBoard(gamePlayManager.state, _mode, 0, startCellFilled);
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
    // Caculate position of numbercell by index of it in array
    Vector2Int SetPositionNumberCell(int _index)
    {
        int positionX = _index / colums + 1;
        int positionY = _index % colums + 1;
        Vector2Int position = new Vector2Int(positionX, positionY);
        return position;
    }

    #region Generateboard
    public void GenerateBoard(int _state, Mode _mode, int _firstIndexWillAddNumber, int _countOfAddNumbers)
    {

        for (int i = 0; i < startCellFilled + (colums - startCellFilled % colums); i++)
        {
            if (i >= startCellFilled && ((i / colums) + 1) % 2 == 0)
            {
                numbers.Add(baseNumbers1[i % colums]);
            }
            else if (i >= startCellFilled && ((i / colums) + 1) % 2 == 1)
            {
                numbers.Add(baseNumbers2[i % colums]);
            }
            else if (((i / colums) + 1) % 2 == 0)
            {
                cells[i].AwakeCell(baseNumbers1[i % colums]);
            }
            else
            {
                cells[i].AwakeCell(baseNumbers2[i % colums]);
            }
        }

        //ShuffleCellValue(startCellFilled);

        int pairNumbersCount = 0;
        if (_state == 1)
        {
            pairNumbersCount = 3;
        }
        else if (_state == 2)
        {
            pairNumbersCount = 2;
        }
        else
        {
            pairNumbersCount = 1;
        }

        ArrangePairNumber(pairNumbersCount);

        //for (int i = 0; i < startCellFilled; i++)
        //{
        //    cells[i].AwakeCell(i % colums + 1);
        //}

        if (_mode == Mode.Gem)
        {
            GenerateGem(_firstIndexWillAddNumber, _countOfAddNumbers);
        }

        gamePlayManager.countAllNumbers = startCellFilled;
    }
    void ShuffleCellValue(int _count)
    {
        for (int i =0; i < _count; i++)
        {
            Cell cell1 = cells[i];

            for (int j = 0; j < 10; j++)
            {
                int indexCell2 = Random.Range(0, startCellFilled);
                Cell cell2 = cells[indexCell2];
                if (cell1.value == cell2.value || cell1.value + cell2.value == minValueOfCell + maxValueOfCell)
                {
                    int temp = cell1.value;
                    cell1.AwakeCell(cell2.value);
                    cell2.AwakeCell(temp);
                    break;
                }
            }
        }
    }
    Cell CanMatchWithCell(Cell _cell)
    {
        foreach (var direction in directions)
        {
            Vector2Int position = _cell.position + direction;
            Cell nextCell = GetCellAt(position);
            if (nextCell == null)
            {

            }
            else if (nextCell.value == _cell.value || nextCell.value + _cell.value == minValueOfCell + maxValueOfCell)
            {
                return nextCell;
            }
        }
        return null;
    }
    Cell GetCellAt(Vector2Int _position)
    {
        foreach (var cell in cells)
        {
            if (cell.position == _position)
            {
                return cell;
            }
        }
        return null;
    }
    void ArrangePairNumber(int _pairNumbersCount)
    {
        for (int i = 0; i < _pairNumbersCount; i++)
        {
            int indexCell1 = Random.Range(0, startCellFilled);
            Cell cell1 = cells[indexCell1];

            for (int j = 0; j < 1000; j++) 
            {
                int indexCell2 = Random.Range(0, startCellFilled);
                Cell cell2 = cells[indexCell2];
                if ( cell1.value != cell2.value && cell1.value + cell2.value != minValueOfCell + maxValueOfCell && !cell1.canMatching && !cell2.canMatching)
                {
                    int temp = cell1.value;
                    cell1.value = cell2.value;
                    cell2.value = temp;

                    Cell cellCanMatchCell1 = CanMatchWithCell(cell1);
                    Cell cellCanMatchCell2 = CanMatchWithCell(cell2);
                    if ( (cellCanMatchCell1 != null) ^ (cellCanMatchCell2 != null) )
                    {
                        cell1.AwakeCell(cell1.value);
                        cell1.canMatching = true;
                        if ( cellCanMatchCell1 != null )
                        {
                            cell1.text.color = Color.gray;
                            cellCanMatchCell1.text.color = Color.gray;
                        }
                        cell2.AwakeCell(cell2.value);
                        cell2.canMatching = true;
                        if (cellCanMatchCell2 != null)
                        {
                            cell2.text.color = Color.gray;
                            cellCanMatchCell2.text.color = Color.gray;
                        }
                        break;
                    }
                    else
                    {
                        cell2.value = cell1.value;
                        cell1.value = temp;
                    }
                }
            }
        }
    }
    #endregion

    #region Handle board
    public void AwakeCells(List<int> _numberList, int _firstAwakeCell)
    {
        for (int i = _firstAwakeCell; i < _firstAwakeCell + _numberList.Count; i++)
        {
            cells[i].AwakeCell(_numberList[i - _firstAwakeCell]);
        }
    }
    public void ClearRowHandle(int _row)
    {
        int startIndex = (_row - 1) * colums;

        if (cells.Count < colums * rows + 1)
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
    #endregion

    #region Generate gem
    public void GenerateGem(int _firstIndexWillAddNumber, int _countOfAddNumbers)
    {
        lastIndexOfCellWillAppearanceGem = Mathf.CeilToInt((_countOfAddNumbers + 1) / 2) + _firstIndexWillAddNumber;
        maxCountOfGemsWhenAddNumbers = gamePlayManager.countOfGemType;
        int currentCountOfGemAppearanced = 0;

        for(int i = _firstIndexWillAddNumber; i < _firstIndexWillAddNumber + _countOfAddNumbers; i++)
        {
            gemTileAppearanceRate = Random.Range(5, 8);
            int random = Random.Range(0, 101);
            bool isGemCell = random < gemTileAppearanceRate;
            if (i == lastIndexOfCellWillAppearanceGem - 1)
            {
                isGemCell = true;
                
            }
            if (isGemCell)
            {
                lastIndexOfCellWillAppearanceGem += i;
                currentCountOfGemAppearanced++;
                HandleGemCell(cells[i]);
                if (currentCountOfGemAppearanced == maxCountOfGemsWhenAddNumbers)
                {
                    lastIndexOfCellWillAppearanceGem = 0;
                    return;
                }
            }
        }
    }
    void HandleGemCell(Cell _cell)
    {
        if (gemType.Count == 0) { return; }

        _cell.isGemCell = true;

        int randomIndex = Random.Range(0, gemType.Count);
        if (gemType[randomIndex] == 1)
        {
            _cell.gemType = GemType.Gem1;
            _cell.button.image.sprite = gemSprite[0];
        }
        else if (gemType[randomIndex] == 2)
        {
            _cell.gemType = GemType.Gem2;
            _cell.button.image.sprite = gemSprite[1];
        }
        gemType.Remove(gemType[randomIndex]);
    }
    #endregion
}
