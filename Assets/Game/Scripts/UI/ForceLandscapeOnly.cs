using UnityEngine;

public class ForceLandscapeOnly : MonoBehaviour
{
    void Awake()
    {
        if (!Application.isMobilePlatform) return;

        Screen.orientation = ScreenOrientation.LandscapeLeft;

        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;

        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }
}