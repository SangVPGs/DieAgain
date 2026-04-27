using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "Game/Level")]
public class LevelData : ScriptableObject
{
    public GroundData[] grounds;
    public Vector3 goalPosition;
}

[System.Serializable]
public class GroundData
{
    public Vector3 startPosition;
    public int length;
}