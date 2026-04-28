using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    void Start()
    {
        UpdateLevelText();
    }

    public void UpdateLevelText()
    {
        int level = GameManager.Instance.CurrentLevel + 1;

        levelText.text = "Level " + level;
    }

    public void BackToMenu()
    {
        AudioManager.Instance.StopBGM();

        PlayerPrefs.SetString("MenuState", MenuState.MainMenu.ToString());
        SceneManager.LoadScene("Menu");
    }
}