using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    private string saveFile;
    public PlayerStats playerStats;
    [SerializeField] private GameObject beatSpawnerController,soundManager;
    public Camera mainCamera;
    public Tilemap tilemap;

    private HashSet<XPDrop> xpList = new HashSet<XPDrop>();

    private void Awake()
    {
        saveFile = Application.persistentDataPath + "/playerInfo.json";
        soundManager = GameObject.FindGameObjectWithTag("SoundManager");
        ReadFile();
        mainCamera = Camera.main;
        Enemy.movementSpeed = 4f;
        Debug.Log("Start");
    }

    private void FixedUpdate()
    {   Enemy.healthProsenIncreas += 0.0000f;
        Enemy.movementSpeed += 0.001f;
    }
    private void ReadFile()
    {
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
        File.WriteAllText(saveFile, playerStats.SaveToString());
        SceneManager.LoadScene("MainMenu");
        soundManager.GetComponent<SoundManager>().Die();

    }

    public BoundsInt GetBoundsFromCamera()
    {
        float cameraSize = mainCamera.orthographicSize;
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3Int minPosition = tilemap.WorldToCell(cameraPosition - new Vector3(cameraSize * mainCamera.aspect, cameraSize, 0));
        Vector3Int maxPosition = tilemap.WorldToCell(cameraPosition + new Vector3(cameraSize * mainCamera.aspect, cameraSize, 0));
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
