using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Floor : MonoBehaviour
{
    public Camera mainCamera;
    public Tilemap tilemap;
    public TileBase tileToPlace;
    public Color[] colors;

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
        BoundsInt bounds = GetBoundsFromCamera();
        for (int x = bounds.min.x; x <= bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y <= bounds.max.y; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(position);
                if (tile == null)
                {
                    PlaceTile(position);
                }
            }
        }

    }

    BoundsInt GetBoundsFromCamera()
    {
        float cameraSize = mainCamera.orthographicSize;
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3Int minPosition = tilemap.WorldToCell(cameraPosition - new Vector3(cameraSize * mainCamera.aspect, cameraSize, 0));
        Vector3Int maxPosition = tilemap.WorldToCell(cameraPosition + new Vector3(cameraSize * mainCamera.aspect, cameraSize, 0));
        

        return new BoundsInt(minPosition, maxPosition - minPosition);
    }
}
