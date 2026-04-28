using UnityEngine;
using UnityEngine.SceneManagement;

public enum MenuState
{
    MainMenu,
    LevelSelect,
    Lose
}

public class MenuUIManager : MonoBehaviour
{
    public static MenuUIManager Instance;

    [Header("Panels")]
    public GameObject menuPanel;
    public GameObject levelPanel;
    public GameObject losePanel;

    [Header("Character")]
    [SerializeField] private CharacterMenuAnimator characterAnimator;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        LoadMenuState();
    }

    private void LoadMenuState()
    {
        string stateString = PlayerPrefs.GetString("MenuState", MenuState.MainMenu.ToString());

        if (System.Enum.TryParse(stateString, out MenuState menuState))
        {
            switch (menuState)
            {
                case MenuState.MainMenu:
                    ShowMenuPanel();
                    break;
                case MenuState.LevelSelect:
                    ShowLevelPanel();
                    break;
                case MenuState.Lose:
                    ShowLosePanel();
                    break;
                default:
                    ShowMenuPanel();
                    break;
            }
        }
        else
        {
            ShowMenuPanel();
        }

        PlayerPrefs.SetString("MenuState", MenuState.MainMenu.ToString());
    }

    public void ShowMenuPanel()
    {
        menuPanel.SetActive(true);
        levelPanel.SetActive(false);
        losePanel.SetActive(false);

        characterAnimator?.ShowAndPlayIdle();
    }

    public void ShowLevelPanel()
    {
        menuPanel.SetActive(false);
        levelPanel.SetActive(true);
        losePanel.SetActive(false);

        characterAnimator?.Hide();
    }

    public void ShowLosePanel()
    {
        menuPanel.SetActive(false);
        levelPanel.SetActive(false);
        losePanel.SetActive(true);

        characterAnimator?.ShowAndPlayDie();
    }

    public void OnPlayButtonClicked()
    {
        ShowLevelPanel();
    }

    public void OnLevelSelected(int levelIndex)
    {
        PlayerPrefs.SetInt("level", levelIndex);
        SceneManager.LoadScene("Gameplay");
    }

    public void OnRetry()
    {
        SceneManager.LoadScene("Gameplay");
    }
}