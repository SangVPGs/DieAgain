using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    [Header("Cài đặt Level Select")]
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private Transform panelContainer;

    private int totalLevels = 20;

    private void Start()
    {
        if (panelContainer == null)
        {
            Debug.LogError("PanelContainer chưa được gán!");
            return;
        }

        GenerateLevelButtons();
    }

    private void GenerateLevelButtons()
    {
        // Xóa hết các button cũ
        foreach (Transform child in panelContainer)
        {
            Destroy(child.gameObject);
        }

        int highestUnlocked = PlayerPrefs.GetInt("highestLevel", 0); // Mặc định mở level đầu

        for (int i = 0; i < totalLevels; i++)
        {
            GameObject btnObj = Instantiate(levelButtonPrefab, panelContainer);
            LevelButton levelBtn = btnObj.GetComponent<LevelButton>();

            if (levelBtn != null)
            {
                bool isUnlocked = (i <= highestUnlocked);
                levelBtn.Setup(i, isUnlocked);
            }
            else
            {
                Debug.LogWarning("LevelButton prefab thiếu script LevelButton!");
            }
        }
    }
}