using UnityEngine;

[CreateAssetMenu(menuName = "Merge/Chip Visual Config")]
public class ChipVisualConfig : ScriptableObject
{
    public Color[] levelColors;

    public Color GetColor(int level)
    {
        int index = level - 1;

        if (index < 0 || index >= levelColors.Length)
            return Color.white;

        return levelColors[index];
    }
}
