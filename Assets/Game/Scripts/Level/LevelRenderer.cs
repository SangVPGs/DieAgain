using UnityEngine;

public class LevelRenderer : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject goalPrefab;
    public float tileSize = 2f;

    public void Render(LevelData level)
    {
        Clear();

        foreach (var ground in level.grounds)
        {
            RenderGround(ground);
        }

        // spawn cổng
        Instantiate(goalPrefab, level.goalPosition, Quaternion.identity, transform);
    }

    void RenderGround(GroundData ground)
    {
        for (int x = 0; x < ground.length; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                Vector3 pos = ground.startPosition + new Vector3(
                    x * tileSize,
                    -tileSize,
                    z * tileSize
                );

                Instantiate(cubePrefab, pos, Quaternion.identity, transform);
            }
        }
    }

    void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}