using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    public PlayerStats playerStats;
    private string playerStatsFile;
    public Camera mainCamera;
    public Tilemap tilemap;
    public int currentTrackBPM;

    private HashSet<XPDrop> xpList = new HashSet<XPDrop>();

    private void Awake()
    {
        playerStatsFile = Application.persistentDataPath + "/playerInfo.json";
        ReadFile(playerStatsFile);
        mainCamera = Camera.main;
        Enemy.movementSpeed = 1f;        // Global % enemy movespeed increase.  
        Enemy.healthProcenIncrease =1f;
    }

    private void Update()
    {
        currentTrackBPM = SoundManager.Instance.GetComponent<SoundManager>().GetCurrentBPM();
    }

    private void FixedUpdate()
    {  
        Enemy.movementSpeed += 0.0001f; // Global % enemy movespeed increase.  
        Enemy.healthProcenIncrease += 0.0001f;
    }

    private void ReadFile(string saveFile)
    {
        playerStats = FindObjectOfType<PlayerStats>();
        if (File.Exists(saveFile))
        {
            string fileContents = File.ReadAllText(saveFile);
            playerStats.GetComponent<PlayerStats>().CreateFromJSON(fileContents);
        }
        else
        {
            Debug.Log("File does not exist.");
        }
    }

    public void GameOver()
    {
        SoundManager.Instance.GetComponent<SoundManager>().Die();
        File.WriteAllText(playerStatsFile, playerStats.SaveToString());
        SceneManager.LoadScene("ResultsScreen");
       
    }

    public BoundsInt GetBoundsFromCamera()
    {
        float cameraSize = mainCamera.orthographicSize;
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3Int minPosition = tilemap.WorldToCell(cameraPosition - new Vector3(cameraSize - 2 * mainCamera.aspect * 2.5f, cameraSize + 10, 0));
        Vector3Int maxPosition = tilemap.WorldToCell(cameraPosition + new Vector3(cameraSize- 5 * mainCamera.aspect , cameraSize + 10, 0));
        return new BoundsInt(minPosition, maxPosition - minPosition);
    }

    public void AddXpObject(XPDrop toAdd){
        xpList.Add(toAdd);
    }
    public void RemoveXpObject(XPDrop toRemove){
        xpList.Remove(toRemove);
    }

    public HashSet<XPDrop> GetXPDropObjects(){
        return xpList;
    }

   
}
