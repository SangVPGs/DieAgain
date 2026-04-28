using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelData[] levels;

    public LevelData CurrentLevel { get; private set; }

    LevelRenderer levelRenderer;

    void Awake()
    {
        levelRenderer = FindFirstObjectByType<LevelRenderer>();
    }

    public void LoadLevel(int index)
    {
        CurrentLevel = levels[index];
        levelRenderer.Render(CurrentLevel);
    }
}