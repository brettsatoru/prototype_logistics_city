using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class World : MonoBehaviour
{
    [SerializeField] private WorldGenerator worldGenerator;
    [SerializeField] private Grid worldGrid;

    private Dictionary<Vector3Int, GameObject> gridPieces;

    public event Action<Vector3Int, GameObject> GridObjectClicked;

    public bool ChangeObjectAtCoord(Vector3Int coord, GameObject gridPrefab)
    {
        if (gridPrefab == null) return false;
        if (!gridPieces.ContainsKey(coord)) return false;

        Vector3 position = worldGrid.CellToWorld(coord);
        var go = Instantiate(gridPrefab, position, Quaternion.identity);

        Destroy(gridPieces[coord]);

        SetupGridObject(coord, go);

        return true;
    }

    private void Awake()
    {
        if (gridPieces == null)
        {
            gridPieces = new Dictionary<Vector3Int, GameObject>();
        }
    }

    private void Start()
    {
        if (worldGenerator != null)
        {
            worldGenerator.GenerateWorld(worldGrid, SetupGridObject);
        }
    }

    private void SetupGridObject(Vector3Int coord, GameObject go)
    {
        var eventTrigger = go.GetComponent<EventTrigger>();
        if (eventTrigger == null) return;

        foreach (var trigger in eventTrigger.triggers)
        {
            switch (trigger.eventID)
            {
                case EventTriggerType.PointerClick:
                    trigger.callback.AddListener((bed) =>
                    {
                        GridPointerClickEvent(coord, go);
                    });
                    break;
                case EventTriggerType.PointerEnter:
                    break;
                case EventTriggerType.PointerExit:
                    break;
                default:
                    continue;
            }
        }

        if (gridPieces.ContainsKey(coord))
        {
            gridPieces[coord] = go;
        }
        else
        {
            gridPieces.Add(coord, go);
        }
    }

    private void GridPointerClickEvent(Vector3Int coord, GameObject gobj)
    {
        GridObjectClicked?.Invoke(coord, gobj);
    }

    private void GridPointerEnterEvent()
    {

    }

    private void GridPointerExitEvent()
    {

    }
}
