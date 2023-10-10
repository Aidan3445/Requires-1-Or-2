using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.InputSystem;

public class GridBehavior : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    public CellBehavior[] cells;
    public MemoryGameManager gameManager;

    CellType[] grid = { CellType.Two, CellType.Six, CellType.Two, CellType.One,
        CellType.Six, CellType.Three, CellType.Five, CellType.Four,
        CellType.Five, CellType.Four, CellType.One, CellType.Three };


    List<int> completedCellIndices;
    int activeCellIndex;

    private void Debug(string message)
    {
        if (debugText)
        {
            debugText.text = message;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeCells();

        // On start the list of indices should be fresh
        completedCellIndices = new List<int>();

        // The active cell should be -1
        activeCellIndex = -1;
    }

    void InitializeCells()
    {
        grid = RandomizeGrid();
        
        // Set the index and type for each cell
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].SetCellIndex(i);
            cells[i].SetCellType(grid[i]);
            cells[i].ShowCellText();
        }
    }

    private CellType[] RandomizeGrid()
    {
        

        int gridSize = grid.Length;
        
        CellType[] newGrid = new CellType[gridSize];

        for (int i = 0; i < gridSize; i++)
        {
            int newGridIndex = Random.Range(0, gridSize);

            while (newGrid[newGridIndex] is not CellType.None)
            {
                newGridIndex = Random.Range(0, gridSize);
            }

            newGrid[newGridIndex] = grid[i];
        }

        return newGrid;
    }

    public void OnCellHitListener(int cellIndex)
    {
        // Game behavior is ignored if the game isn't even active
        if (!gameManager.GetGameStatus())
        {
            return;
        }

        // If active cell is -1, there is no currently selected cell
        if (activeCellIndex == -1)
        {
            // If it's not already a completed cell
            if (!completedCellIndices.Contains(cellIndex))
            {
                // Set the active cell to the given one
                activeCellIndex = cellIndex;
                cells[cellIndex].Selected();
                Debug("Setting " + cellIndex + " active");
                gameManager.UpdateScoreboardBody("Find the matching cell for " + CellBehavior.CellToString(grid[cellIndex]));
            }
            // If it is already a completed cell, ignore
        }
        // Otherwise there is an active cell
        else
        {
            // If it's a unique index and a not already completed cell
            if (activeCellIndex != cellIndex && !completedCellIndices.Contains(cellIndex))
            {
                // Check to see if the two cells match
                if (grid[activeCellIndex] == grid[cellIndex])
                {
                    // They do match, add to the completed cells
                    completedCellIndices.Add(activeCellIndex);
                    completedCellIndices.Add(cellIndex);
                    Debug(activeCellIndex + " and " + cellIndex + " match");
                    gameManager.UpdateScoreboardBody("Correct: " + CellBehavior.CellToString(grid[activeCellIndex]) + " and " + CellBehavior.CellToString(grid[cellIndex]) + " match!");
                    // Call the success methods
                    cells[activeCellIndex].Success();
                    cells[cellIndex].Success();
                    // Success in game manager
                    gameManager.Success();
                    // Reset the active cell index
                    activeCellIndex = -1;
                }
                // If the cells don't match
                else
                {
                    // Don't add to the completed cells
                    Debug(activeCellIndex + " and " + cellIndex + " don't match");
                    gameManager.UpdateScoreboardBody("Incorrect: " + CellBehavior.CellToString(grid[activeCellIndex]) + " and " + CellBehavior.CellToString(grid[cellIndex]) + " don't match");
                    // Call the fail methods
                    cells[activeCellIndex].Failed();
                    cells[cellIndex].Failed();
                    // Penalize in game manager
                    gameManager.Penalize();
                    // Reset the active cell index
                    activeCellIndex = -1;
                }
            }
            // If it is the same as the active or already a completed cell, ignore
        }
        // Game is complete if all pairs have been added to the completed list
        if (completedCellIndices.Count == cells.Length)
        {
            gameManager.GameOver();
        }
    }

    public void HideAllCellText()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].HideCellText();
        }
    }

    public int GetTotalCells()
    {
        return cells.Length;
    }
}
