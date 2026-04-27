using System.Collections;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        StartCoroutine(LoadNext()); // Đợi 1 frame trước khi load level tiếp theo để tránh lỗi va chạm liên tục
    }

    IEnumerator LoadNext()
    {
        yield return null; // Đợi 1 frame

        LevelManager.Instance.NextLevel();
    }
}