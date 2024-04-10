using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public Camera mainCamera;
    public Tilemap tilemap;
    public GameObject enemy;
    public GameObject parent;
    private bool waveHasSpawned;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if (!waveHasSpawned) { 
            waveHasSpawned = true;
            Invoke("SpawnNextWave", 10);
            BoundsInt bounds = GetBoundsFromCamera();
            Debug.Log("");
            
            Debug.Log("bounds: " + bounds);
            for (int x = bounds.min.x; x <= bounds.max.x; x++)
            {
                if (x == bounds.min.x || x == bounds.max.x) { 
                    for (int y = bounds.min.y; y <= bounds.max.y; y++)
                    {
                        if (y == bounds.min.y || y == bounds.max.y) {
                            Vector3Int position = new Vector3Int(x, y, 0);
                            TileBase tile = tilemap.GetTile(position);
                            if (tile == null)
                            {
                                Instantiate(enemy, position, Quaternion.identity, parent.GetComponent<Transform>());
                            }
                        }
                    }
                }
            }
        }
    }

    void SpawnNextWave()
    {
        waveHasSpawned = false;
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
