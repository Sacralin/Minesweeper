using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloodFillGrid : MonoBehaviour
{
    public GameObject cellPrefab;
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float cellSize = 1.1f;
    public Color clearCellColor = Color.white;
    public Color mineCellColor = Color.red;

    private Cell[,] grid;

    

    private void Start()
    {
        GenerateGrid();

        

    }

    private void GenerateGrid()
    {
        grid = new Cell[gridWidth, gridHeight];

        for (int x = 0;  x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GameObject newCell = Instantiate(cellPrefab, new Vector3 (x * cellSize - 5, y * cellSize - 5, 0), Quaternion.identity);
                newCell.transform.SetParent (transform);

                bool isMine = UnityEngine.Random.Range(0, 3) == 0;

                Cell cell = newCell.AddComponent<Cell>();
                cell.Init(x, y, isMine, this);

                if(isMine)
                {
                    cell.SetColor(mineCellColor);
                }
                else
                {
                    cell.SetColor(clearCellColor);
                }
                grid[x,y] = cell;
            }
        }
    }

    public void FloodFill(int startX, int startY)
    {
        if (grid[startX, startY].isMine) return;

        Stack<Cell> stack = new Stack<Cell>();
        stack.Push(grid[startX, startY]);

        while(stack.Count > 0)
        {
            Cell cell = stack.Pop();
            if (cell.isRevealed || cell.isMine) continue;

            cell.reveal();

            CheckAndAddCellToStack(cell.x +1, cell.y, stack);
            CheckAndAddCellToStack(cell.x -1, cell.y, stack);
            CheckAndAddCellToStack(cell.x, cell.y +1, stack);
            CheckAndAddCellToStack(cell.x, cell.y -1, stack);
        }

    }

    private void CheckAndAddCellToStack(int x, int y, Stack<Cell> stack)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight && !grid[x, y].isRevealed && !grid[x, y].isMine)
        {
            stack.Push(grid[x, y]);
        }
    }

    private void CheckForAdjacentMines(Cell cell)
    {
        

    
    }
}
