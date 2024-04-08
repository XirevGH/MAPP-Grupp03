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
            tilemap.SetColor(position, colors[Random.Range(0, colors.Length)]);
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
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 cameraSize = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Mathf.Abs(cameraPosition.z)));
        Vector3Int minPosition = tilemap.WorldToCell(cameraPosition - cameraSize);
        Vector3Int maxPosition = tilemap.WorldToCell(cameraPosition + cameraSize);

        return new BoundsInt(minPosition, maxPosition - minPosition);
    }
}
