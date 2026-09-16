using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Floor : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tileToPlace;
    public Color[] colors;
    public GameController gameController;

    [Header("Settings")]
    [Tooltip("Extra margin (in tiles) outside the camera before tiles get deleted.")]
    public int despawnBuffer = 2;

    void PlaceTile(Vector3Int position)
    {
        if (tilemap != null && tileToPlace != null)
        {
            tilemap.SetTile(position, tileToPlace);
            tilemap.SetTileFlags(position, TileFlags.None);

            int colorChoice = Random.Range(0, colors.Length * 2);
            if (colorChoice >= colors.Length)
            {
                if (colorChoice % 2 == 1)
                {
                    colorChoice = colors.Length - 1;
                }
                else
                {
                    colorChoice = colors.Length - 2;
                }
            }
            tilemap.SetColor(position, colors[colorChoice]);
        }
        else
        {
            Debug.LogError("Tilemap or tile to place is null!");
        }
    }

    private void Update()
    {
        BoundsInt cameraBounds = gameController.GetBoundsFromCamera();

        for (int x = cameraBounds.min.x; x <= cameraBounds.max.x; x++)
        {
            for (int y = cameraBounds.min.y; y <= cameraBounds.max.y; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                if (!tilemap.HasTile(position))
                {
                    PlaceTile(position);
                }
            }
        }

        RemoveOutOfBoundsTiles(cameraBounds);
    }

    private void RemoveOutOfBoundsTiles(BoundsInt cameraBounds)
    {
        int minX = cameraBounds.min.x - despawnBuffer;
        int maxX = cameraBounds.max.x + despawnBuffer;
        int minY = cameraBounds.min.y - despawnBuffer;
        int maxY = cameraBounds.max.y + despawnBuffer;

        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                if (pos.x < minX || pos.x > maxX || pos.y < minY || pos.y > maxY)
                {
                    tilemap.SetTile(pos, null);
                }
            }
        }

        tilemap.CompressBounds();
    }
}