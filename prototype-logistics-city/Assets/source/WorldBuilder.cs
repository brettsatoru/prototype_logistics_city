using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    [SerializeField] private WorldGenerator worldGenerator;
    [SerializeField] private Grid worldGrid;

    private void Start()
    {
        if (worldGenerator != null)
        {
            worldGenerator.GenerateWorld(worldGrid);
        }
    }
}
