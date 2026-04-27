using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelData[] levels;
    public LevelRenderer levelRenderer;

    public LevelData CurrentLevel { get; private set; }

    public void LoadLevel(int index)
    {
        if (levels == null || levels.Length == 0)
        {
            Debug.LogError("No levels!");
            return;
        }

        if (index < 0 || index >= levels.Length)
        {
            Debug.LogError("Invalid level index!");
            return;
        }

        CurrentLevel = levels[index];

        levelRenderer.Render(CurrentLevel);
    }
}