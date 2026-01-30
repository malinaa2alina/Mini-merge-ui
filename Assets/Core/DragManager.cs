using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private MergeManager mergeManager;

    private Chip draggedChip;
    private Cell startCell;

    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;

    private void Awake()
    {
        raycaster = canvas.GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(null);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPickChip();
        }
        else if (Input.GetMouseButton(0) && draggedChip != null)
        {
            DragChip();
        }
        else if (Input.GetMouseButtonUp(0) && draggedChip != null)
        {
            ReleaseChip();
        }
    }

    private void TryPickChip()
    {
        pointerEventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        foreach (var r in results)
        {
            Chip chip = r.gameObject.GetComponent<Chip>();
            if (chip != null)
            {
                draggedChip = chip;
                startCell = chip.CurrentCell;

                draggedChip.RectTransform.SetParent(canvas.transform, true);
                draggedChip.RectTransform.SetAsLastSibling();
                break;
            }
        }
    }

    private void DragChip()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out Vector2 pos
        );

        draggedChip.RectTransform.anchoredPosition = pos;
    }

    private void ReleaseChip()
    {
        Cell targetCell = GetCellUnderCursor();

        if (targetCell == null || targetCell == startCell)
        {
            draggedChip.SetCell(startCell);
        }
        else if (targetCell.IsEmpty)
        {
            draggedChip.SetCell(targetCell);
        }
        else if (
            targetCell.CurrentChip != null &&
            targetCell.CurrentChip != draggedChip &&
            targetCell.CurrentChip.Level == draggedChip.Level
        )
        {
            mergeManager.Merge(draggedChip, targetCell.CurrentChip, targetCell);
        }
        else
        {
            draggedChip.SetCell(startCell);
        }

        draggedChip = null;
    }

    private Cell GetCellUnderCursor()
    {
        pointerEventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        foreach (var r in results)
        {
            Cell cell = r.gameObject.GetComponent<Cell>();
            if (cell != null)
                return cell;
        }

        return null;
    }
}
