using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public LevelManager levelManager;
    public PlayerController player;

    private int currentLevel;
    private int highestLevel;

    public int CurrentLevel => currentLevel;

    LevelRenderer levelRenderer;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        levelRenderer = FindFirstObjectByType<LevelRenderer>();

        currentLevel = PlayerPrefs.GetInt("level", 0);
        LoadLevel();
    }

    void LoadLevel()
    {
        levelManager.LoadLevel(currentLevel);

        var level = levelManager.CurrentLevel;
        player.ResetState(level.spawnPosition);

        FindFirstObjectByType<GameUIManager>()?.UpdateLevelText();
        AudioManager.Instance.PlayBGM();
    }

    public void OnTrapTriggered(int x)
    {
        var row = levelRenderer.GetRow(x);
        if (row == null) return;

        foreach (var tile in row)
        {
            tile.Fall();
        }
    }

    public void OnPlayerReachGoal()
    {
        AudioManager.Instance.PlayGoal();
        StartCoroutine(WinFlow());
    }

    IEnumerator WinFlow()
    {
        player.DisableInput();

        yield return new WaitForSeconds(0.5f);

        currentLevel++;

        if (currentLevel >= levelManager.levels.Length)
            currentLevel = 0;

        PlayerPrefs.SetInt("level", currentLevel);

        if (currentLevel > highestLevel)
        {
            highestLevel = currentLevel;
            PlayerPrefs.SetInt("highestLevel", highestLevel);
        }

        LoadLevel();

        player.EnableInput();
    }

    public void OnPlayerDead()
    {
        AudioManager.Instance.PlayDie();
        StartCoroutine(LoseFlow());
    }

    IEnumerator LoseFlow()
    {
        player.DisableInput();

        AudioManager.Instance.PlayLose();

        yield return new WaitForSeconds(5f);

        PlayerPrefs.SetString("MenuState", MenuState.Lose.ToString());
        SceneManager.LoadScene("Menu");
    }
}