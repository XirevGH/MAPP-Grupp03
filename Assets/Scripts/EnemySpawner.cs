using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public Camera mainCamera;
    public Tilemap tilemap;
    public GameObject enemy;
    public GameObject parent;
    void Start()
    {
        
        BoundsInt bounds = GetBoundsFromCamera();
        for (int x = bounds.min.x; x <= bounds.max.x; x++)
        {
            Vector3Int position = new Vector3Int(x, bounds.max.y, 0);
            TileBase tile = tilemap.GetTile(position);
            if (tile == null)
            {
                Instantiate(enemy, position, Quaternion.identity, parent.GetComponent<Transform>());
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    BoundsInt GetBoundsFromCamera()
    {
        float cameraSize = mainCamera.orthographicSize * 2;
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3Int minPosition = tilemap.WorldToCell(cameraPosition - new Vector3(cameraSize * mainCamera.aspect, cameraSize, 0));
        Vector3Int maxPosition = tilemap.WorldToCell(cameraPosition + new Vector3(cameraSize * mainCamera.aspect, cameraSize, 0));


        return new BoundsInt(minPosition, maxPosition - minPosition);
    }
}
