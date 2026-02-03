using UnityEngine;
using UnityEngine.UI;

public class Chip : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image image;
    [SerializeField] private Color[] levelColors;

    public int Level { get; private set; }
    public RectTransform RectTransform { get; private set; }
    public Cell CurrentCell { get; private set; }
    public Cell StartCell { get; private set; }

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();

        if (image == null)
            image = GetComponent<Image>();
    }

    public void Init(int level, Cell cell)
    {
        Level = level;
        UpdateVisual();
        SetCell(cell);
    }

    public void BeginDrag()
    {
        StartCell = CurrentCell;
    }

    private void UpdateVisual()
    {
        int index = Mathf.Clamp(Level - 1, 0, levelColors.Length - 1);
        image.color = levelColors[index];
    }

    public void SetCell(Cell cell)
    {
        if (CurrentCell != null)
            CurrentCell.Clear();

        CurrentCell = cell;
        cell.SetChip(this);

        RectTransform.SetParent(cell.transform, false);
        RectTransform.anchoredPosition = Vector2.zero;
        RectTransform.localScale = Vector3.one;
    }
}
