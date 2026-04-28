using UnityEngine;

public class PlatformChecker : MonoBehaviour
{
    public GameObject[] objectsToHide;

    void Start()
    {
        if (!Application.isMobilePlatform)
        {
            foreach (var obj in objectsToHide)
            {
                obj.SetActive(false);
            }
        }
    }
}