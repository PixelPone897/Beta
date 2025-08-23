using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectionAreaRenderer : MonoBehaviour
{
    [SerializeField] private TileBase highlightTile;
    private Tilemap tilemap;
    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void Render(IEnumerable<Vector3Int> positions)
    {
        tilemap.ClearAllTiles();
        foreach (var pos in positions)
        {
            tilemap.SetTile(pos, highlightTile);
        }
    }

    public void Clear()
    {
        tilemap.ClearAllTiles();
    }

    public Vector3 GetCellCenterWorldPosition(Vector3Int position)
    {
        return tilemap.GetCellCenterWorld(position);
    }
 }
