using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Button button;
    [SerializeField] private Image lockImage;

    private int levelIndex;

    private void Awake()
    {
        if (button == null) button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void Setup(int index, bool isUnlocked)
    {
        levelIndex = index;
        levelText.text = (index + 1).ToString();

        button.interactable = isUnlocked;

        if (lockImage != null)
            lockImage.gameObject.SetActive(!isUnlocked);

        if (!isUnlocked)
        {
            levelText.color = new Color(1f, 1f, 1f, 0.4f);
        }
        else
        {
            levelText.color = Color.white;
        }
    }

    private void OnClick()
    {
        MenuUIManager.Instance.OnLevelSelected(levelIndex);
    }
}