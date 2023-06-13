using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "WorldGen", menuName = "CityLogistics/WorldGen/World Gen")]
public class WorldGenerator : ScriptableObject
{
    [SerializeField] private GameObject BlankTile;

    [SerializeField] private GameObject ForestTile;
    [SerializeField] private float forestTileWeight;

    [Header("Generator Data")]
    [SerializeField] int seed;
    [SerializeField] int height;
    [SerializeField] int width;

    public void GenerateWorld(Grid worldGrid)
    {
        if (worldGrid == null) return;
        if (seed == 0)
        {
            seed = (int)DateTime.Now.Ticks * height * width;
        }
        UnityEngine.Random.InitState(seed);

        for (int x = 0 - width / 2; x < width / 2; x++)
        {
            for (int y = 0 - height / 2; y < height / 2; y++)
            {
                // Get random number
                float tRandom = UnityEngine.Random.Range(0.0f, 1.0f);
                GameObject prefab2Gen = BlankTile;
                if (tRandom <= forestTileWeight)
                {
                    prefab2Gen = ForestTile;
                }

                Instantiate(prefab2Gen, worldGrid.CellToWorld(new Vector3Int(x, y, 0)), Quaternion.identity);
            }
        }
    }
}
