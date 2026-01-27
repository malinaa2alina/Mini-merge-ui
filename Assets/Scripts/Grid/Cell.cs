using System.Collections;
using System.Collections.Generic;
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
        chip.transform.SetParent(transform);
        chip.RectTransform.anchoredPosition = Vector2.zero;
    }

    public void Clear()
    {
        CurrentChip = null;
    }
}
