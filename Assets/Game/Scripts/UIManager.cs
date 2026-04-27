using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    void Update()
    {
        UpdateLevelText();
    }

    public void UpdateLevelText()
    {
        int level = GameManager.Instance.CurrentLevel + 1;

        levelText.text = "Level " + level;
    }
}