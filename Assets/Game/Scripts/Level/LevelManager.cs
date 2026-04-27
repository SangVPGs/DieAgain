using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Levels")]
    public LevelData[] levels;

    [Header("Refs")]
    public LevelRenderer levelRenderer;
    public Transform player;

    int currentLevel;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("level", 0);
        LoadLevel(currentLevel);
    }

    public void LoadLevel(int index)
    {
        if (levels == null || levels.Length == 0)
        {
            Debug.LogError("Không có level!");
            return;
        }

        currentLevel = index;

        levelRenderer.Render(levels[currentLevel]);

        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        player.position = new Vector3(0, 1, 2); // giữa lane
    }

    public void NextLevel()
    {
        currentLevel++;

        if (currentLevel >= levels.Length)
        {
            currentLevel = 0;
        }

        PlayerPrefs.SetInt("level", currentLevel);

        LoadLevel(currentLevel);
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevel);
    }
}