using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

[System.Serializable]  
public class BossWaves
{
    public GameObject enemyBOSS;
    public int bossWave;
}



public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<BossWaves> bossWaves = new List<BossWaves>();
    private bool waveHasSpawned;
    public float spawnIncreaser;
    public static bool bossAlive;
    public Camera mainCamera;
    public Tilemap tilemap;
    public GameObject enemyBouncer;
    public GameObject enemyDancer;
    public GameObject enemyDrunkard;

    public GameObject setNormalEnemyParent;
    public GameObject setBossEnemyParent;

    public static GameObject normalEnemyParent;
    public static GameObject bossEnemyParent;

    public GameObject[] spawnLocations;
    public GameObject circleSpawnRotationPoint;
    public GameObject player;

    public float spawnRate;

    public int waveCount = 0;

    private void Start() {
        waveHasSpawned = false;
        bossAlive = false;
        normalEnemyParent = setNormalEnemyParent;
        bossEnemyParent = setBossEnemyParent;
    }

    
   void Update()
    {   
        if(!bossAlive){ //Stops vave spwaning if ther is a boss alive
            if(!waveHasSpawned){
                SpawnWave();
                waveHasSpawned = true;
                Invoke("SpawnNextWave", spawnRate);
            }
            
        }
    }

    void SpawnWave()
    {       
        if (IsBossWave()) {
            //1 is how meny bosses spawn
            SpawnEnemiesInCircle(1);
            
        } else {
            SpawnEnemiesInCircle(1 + (int)spawnIncreaser);
            
        }
        waveCount += 1;
    }

    void SpawnNextWave()
    {
        waveHasSpawned = false;
    }

    private void FixedUpdate()
    {
        if(!bossAlive){
            spawnRate -= 0.00001f;
            spawnIncreaser += 0.0006f;}
    }
    void SpawnEnemy(Vector3Int position)
    {   
         
        if(IsBossWave()){
            Instantiate(GetBossToSpawn(), position, Quaternion.identity, bossEnemyParent.GetComponent<Transform>());
        }else{
            if(waveCount < 60){
                Instantiate(enemyDrunkard, position, Quaternion.identity, normalEnemyParent.GetComponent<Transform>());
            }else if(waveCount < 120){
                Instantiate(enemyDancer, position, Quaternion.identity, normalEnemyParent.GetComponent<Transform>());
            }else{
                Instantiate(enemyBouncer, position, Quaternion.identity, normalEnemyParent.GetComponent<Transform>());
            }
        }
        
        
    }

    

/*    void SpawnEnemiesInCorners()
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
    }*/

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
        circleSpawnRotationPoint.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Random.Range(0f, 361f));
        for (int i = 0; i < amount; i++) 
        {
            Bounds bound = spawnLocations[Random.Range(0, spawnLocations.Length)].GetComponent<Collider2D>().bounds;
            Vector2 randomPoint = new Vector2(
            Random.Range(bound.min.x, bound.max.x),
            Random.Range(bound.min.y, bound.max.y)
            );
            SpawnEnemy(new Vector3Int((int)randomPoint.x, (int)randomPoint.y, (int)0));
            
        }
    }

    //checks is it is a boss wave
    public bool IsBossWave(){
        foreach (BossWaves boss in bossWaves){
            if(waveCount == boss.bossWave){
                return true;
            }
        }
        return false;
    }

    //returns the Boss a prefab if it is its bossWave 
    private GameObject GetBossToSpawn(){
        foreach (BossWaves boss in bossWaves){
            if(waveCount == boss.bossWave){
                return boss.enemyBOSS;
                
            }
        }
        return null;
    }
}
