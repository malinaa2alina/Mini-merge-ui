using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chip : MonoBehaviour
{
    public int Level { get; private set; }
    public RectTransform RectTransform { get; private set; }
    public Cell CurrentCell { get; private set; }

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    public void Init(int level, Cell cell)
    {
        Level = level;
        SetCell(cell);
    }

    public void SetCell(Cell cell)
    {
        if (CurrentCell != null)
        {
            CurrentCell.Clear();
        }

        CurrentCell = cell;
        cell.SetChip(this);
    }
}
