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
        Chip chip = Instantiate(chipPrefab);
        chip.Init(level, cell);
        Debug.Log($"Spawned chip level {level}");
        return chip;
    }

    public void SpawnRandomChip()
    {
        List<Cell> empty = new();

        foreach (var c in cells)
            if (c.IsEmpty)
                empty.Add(c);

        if (empty.Count == 0)
            return;

        Cell target = empty[Random.Range(0, empty.Count)];
        SpawnChip(target, 1);
    }

    private void SpawnTestChips()
    {
        int spawned = 0;

        foreach (var cell in cells)
        {
            if (!cell.IsEmpty) continue;

            SpawnChip(cell, 1);
            spawned++;

            if (spawned >= 2)
                break;
        }
    }
}
