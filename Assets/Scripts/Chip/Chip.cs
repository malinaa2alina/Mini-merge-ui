using UnityEngine;

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
        CurrentCell = cell;
        cell.SetChip(this);

        RectTransform.SetParent(cell.transform, false);
        RectTransform.anchoredPosition = Vector2.zero;
        RectTransform.localScale = Vector3.one;
    }
}
