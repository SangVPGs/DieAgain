using UnityEngine;
using System.Collections.Generic;

public class LevelRenderer : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject goalPrefab;

    public float tileSize = 2f;

    Dictionary<int, List<GroundTile>> tilesByX = new Dictionary<int, List<GroundTile>>();

    public void Render(LevelData level)
    {
        Clear();
        tilesByX.Clear();

        foreach (var ground in level.grounds)
        {
            RenderGround(ground, level);
        }

        Instantiate(goalPrefab, level.goalPosition, Quaternion.identity, transform);
    }

    void RenderGround(GroundData ground, LevelData level)
    {
        int baseX = Mathf.RoundToInt(ground.startPosition.x / tileSize);

        for (int x = 0; x < ground.length; x++)
        {
            int globalX = baseX + x;
            bool isTrap = IsTrap(level, globalX);

            for (int z = 0; z < 3; z++)
            {
                Vector3 pos = ground.startPosition + new Vector3(
                    x * tileSize,
                    -tileSize,
                    z * tileSize
                );

                var obj = Instantiate(cubePrefab, pos, Quaternion.identity, transform);

                var tile = obj.GetComponent<GroundTile>();
                tile.Init(globalX, isTrap);

                // Lưu theo hàng X
                if (!tilesByX.ContainsKey(globalX))
                    tilesByX[globalX] = new List<GroundTile>();

                tilesByX[globalX].Add(tile);
            }
        }
    }

    bool IsTrap(LevelData level, int x)
    {
        foreach (var t in level.fallingTraps)
        {
            if (t.posXTrigger == x) return true;
        }
        return false;
    }

    public List<GroundTile> GetRow(int x)
    {
        if (!tilesByX.ContainsKey(x)) return null;
        return tilesByX[x];
    }

    void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}