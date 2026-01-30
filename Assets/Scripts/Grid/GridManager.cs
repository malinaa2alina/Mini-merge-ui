using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Chip chipPrefab;
    [SerializeField] private Transform gridRoot;

    private List<Cell> cells = new List<Cell>();

    private void Awake()
    {
        cells.AddRange(gridRoot.GetComponentsInChildren<Cell>());
    }

    private void Start()
    {
        SpawnTestChips();
    }

    public Chip SpawnChip(Cell cell, int level)
    {
        if (cell == null)
        {
            Debug.LogError("SpawnChip: cell is null");
            return null;
        }

        if (!cell.IsEmpty)
        {
            Debug.LogError("SpawnChip: cell is NOT empty");
            return null;
        }

        Chip chip = Instantiate(chipPrefab, gridRoot); 
        chip.Init(level, cell);

        Debug.Log("Spawned chip level " + level);
        return chip;
    }

    private void SpawnTestChips()
    {
        int spawned = 0;

        foreach (var cell in cells)
        {
            if (cell.IsEmpty)
            {
                SpawnChip(cell, 1);
                spawned++;

                if (spawned >= 2)
                    break;
            }
        }
    }
}
