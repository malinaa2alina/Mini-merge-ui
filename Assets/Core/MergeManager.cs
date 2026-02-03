using UnityEngine;
using System.Collections;

public class MergeManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private int maxLevel = 6;

    public void Merge(Chip from, Chip to, Cell targetCell)
    {
        if (to.Level >= maxLevel)
        {
            from.SetCell(from.CurrentCell);
            return;
        }

        int newLevel = from.Level + 1;

        from.CurrentCell.Clear();
        to.CurrentCell.Clear();

        Destroy(from.gameObject);
        Destroy(to.gameObject);

        Chip newChip = gridManager.SpawnChip(targetCell, newLevel);
        AnimateMerge(newChip);
    }


    private void AnimateMerge(Chip chip)
    {
        if (chip == null)
            return;

        chip.StartCoroutine(ScaleRoutine(chip));
    }

    private IEnumerator ScaleRoutine(Chip chip)
    {
        Vector3 start = Vector3.one;
        Vector3 peak = Vector3.one * 1.3f;

        float duration = 0.15f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            chip.RectTransform.localScale =
                Vector3.Lerp(start, peak, t / duration);
            yield return null;
        }

        t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            chip.RectTransform.localScale =
                Vector3.Lerp(peak, start, t / duration);
            yield return null;
        }

        chip.RectTransform.localScale = start;
    }
}
