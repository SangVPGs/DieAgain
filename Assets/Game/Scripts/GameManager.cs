using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public LevelManager levelManager;
    public PlayerController player;

    int currentLevel;
    public int CurrentLevel => currentLevel;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        Goal.OnPlayerReachGoal += OnWin;
    }

    void OnDisable()
    {
        Goal.OnPlayerReachGoal -= OnWin;
    }

    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("level", 0);
        LoadCurrentLevel();
    }

    void LoadCurrentLevel()
    {
        levelManager.LoadLevel(currentLevel);

        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        var level = levelManager.CurrentLevel;

        player.transform.position = level.spawnPosition;
    }

    void OnWin()
    {
        StartCoroutine(WinFlow());
    }

    IEnumerator WinFlow()
    {
        player.canInput = false;

        yield return new WaitForSeconds(0.2f);

        currentLevel++;

        if (currentLevel >= levelManager.levels.Length)
            currentLevel = 0;

        PlayerPrefs.SetInt("level", currentLevel);

        LoadCurrentLevel();

        player.canInput = true;
    }
}