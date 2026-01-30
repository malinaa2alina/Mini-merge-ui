using UnityEngine;

public class Cell : MonoBehaviour
{
    public RectTransform RectTransform { get; private set; }
    public Chip CurrentChip { get; private set; }

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    public bool IsEmpty => CurrentChip == null;

    public void SetChip(Chip chip)
    {
        CurrentChip = chip;
    }

    public void Clear()
    {
        CurrentChip = null;
    }
}
