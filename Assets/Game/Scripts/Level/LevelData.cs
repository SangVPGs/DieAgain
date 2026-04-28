using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "Game/Level")]
public class LevelData : ScriptableObject
{
    [Header("Player")]
    public Vector3 spawnPosition;

    [Header("Map")]
    public GroundData[] grounds;

    public FallingTrapData[] fallingTraps;

    public Vector3 goalPosition;
}

[System.Serializable]
public class GroundData
{
    public Vector3 startPosition;
    public int length;
}

[System.Serializable]
public class FallingTrapData
{
    public int posXTrigger;
}