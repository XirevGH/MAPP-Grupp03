using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    public Camera mainCamera;
    public Tilemap tilemap;
    public GameObject enemy;
    public GameObject parent;
    public GameObject[] spawnLocations;
    
    public int spawnRate;
    private bool waveHasSpawned;

    void Update()
    {
        if (!waveHasSpawned)
        {
            waveHasSpawned = true;
            Invoke("SpawnNextWave", spawnRate);
            SpawnEnemiesInCircle(10);
            SpawnEnemiesInCorners();
        }
    }

    void SpawnEnemy(Vector3Int position)
    {
        Instantiate(enemy, position, Quaternion.identity, parent.GetComponent<Transform>());
    }

    void SpawnEnemiesInCorners()
    {
            BoundsInt bounds = GetBoundsFromCamera();

            for (int x = bounds.min.x; x <= bounds.max.x; x++)
            {
                if (x == bounds.min.x || x == bounds.max.x)
                {
                    for (int y = bounds.min.y; y <= bounds.max.y; y++)
                    {
                        if (y == bounds.min.y || y == bounds.max.y)
                        {
                            Vector3Int position = new Vector3Int(x, y, 0);
                            TileBase tile = tilemap.GetTile(position);
                            if (tile == null)
                            {
                                SpawnEnemy(position);
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

    void SpawnEnemiesInCircle(int amount)
    {
        spawnLocations[0].transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Random.Range(0f, 361f));
        for (int i = 0; i < amount; i++) 
        {
            Bounds bound = spawnLocations[0].GetComponent<CircleCollider2D>().bounds;
            Vector3 randomPoint = new Vector3(
            Random.Range(bound.min.x, bound.max.x),
            Random.Range(bound.min.y, bound.max.y),
            Random.Range(bound.min.z, bound.max.z)
            );
            SpawnEnemy(new Vector3Int((int)randomPoint.x, (int)randomPoint.y, (int)randomPoint.z));
        }
    }
}
