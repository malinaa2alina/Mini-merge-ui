using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Chip chipPrefab;
    [SerializeField] private Transform gridRoot;

    private List<Cell> _cells = new List<Cell>();

    private void Awake()
    {
        gridRoot.GetComponentsInChildren(_cells);
    }

    private void Start()
    {
        SpawnTestChip();
    }

    private void SpawnTestChip()
    {
        foreach (var cell in _cells)
        {
            if (cell.IsEmpty)
            {
                Chip chip = Instantiate(chipPrefab);
                chip.Init(1, cell);
                break;
            }
        }
    }
}
