using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private Chip _draggedChip;
    private Vector2 _startPosition;
    private Cell _startCell;

    private GraphicRaycaster _raycaster;
    private PointerEventData _pointerEventData;

    private void Awake()
    {
        _raycaster = canvas.GetComponent<GraphicRaycaster>();
        _pointerEventData = new PointerEventData(null);
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPickChip();
        }
        else if (Input.GetMouseButton(0) && _draggedChip != null)
        {
            DragChip();
        }
        else if (Input.GetMouseButtonUp(0) && _draggedChip != null)
        {
            ReleaseChip();
        }
    }
    private void TryPickChip()
    {
        _pointerEventData.position = Input.mousePosition;

        var results = new List<RaycastResult>();
        _raycaster.Raycast(_pointerEventData, results);

        foreach (var result in results)
        {
            Chip chip = result.gameObject.GetComponent<Chip>();
            if (chip != null)
            {
                _draggedChip = chip;
                _startCell = chip.CurrentCell;
                _startPosition = chip.RectTransform.anchoredPosition;

                chip.transform.SetAsLastSibling(); // поверх всего
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
            out Vector2 localPoint
        );

        _draggedChip.RectTransform.anchoredPosition = localPoint;
    }

    private void ReleaseChip()
    {
        Cell targetCell = GetCellUnderCursor();

        if (targetCell == null)
        {
            ReturnToStart();
        }
        else if (targetCell.IsEmpty)
        {
            _draggedChip.SetCell(targetCell);
        }
        else if (targetCell.CurrentChip.Level == _draggedChip.Level)
        {
            ReturnToStart(); // временно
        }
        else
        {
            ReturnToStart();
        }

        _draggedChip = null;
    }

    private void ReturnToStart()
    {
        _draggedChip.SetCell(_startCell);
    }

    private Cell GetCellUnderCursor()
    {
        _pointerEventData.position = Input.mousePosition;

        var results = new List<RaycastResult>();
        _raycaster.Raycast(_pointerEventData, results);

        foreach (var result in results)
        {
            Cell cell = result.gameObject.GetComponent<Cell>();
            if (cell != null)
                return cell;
        }

        return null;
    }
}
