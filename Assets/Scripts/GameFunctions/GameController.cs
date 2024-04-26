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
    [SerializeField] private GameObject triggerController, soundManager, trackswapper;
    public Camera mainCamera;
    public Tilemap tilemap;
    public int currentTrackBPM;

    private HashSet<XPDrop> xpList = new HashSet<XPDrop>();

    private void Awake()
    {
        saveFile = Application.persistentDataPath + "/playerInfo.json";
        soundManager = GameObject.FindGameObjectWithTag("SoundManager");
        ReadFile();
        mainCamera = Camera.main;
        Enemy.movementSpeed = 1f;        // är % * till thisEnmey
        Debug.Log("Start");
    }
    private void Update()
    {
        currentTrackBPM = soundManager.GetComponent<SoundManager>().BPMforTracks[trackswapper.GetComponent<TrackSwapper>().i];
    }
    private void FixedUpdate()
    {  
        Enemy.movementSpeed += 0.0001f; // är % * till thisEnmey
       
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
        SceneManager.LoadScene("ResultsScreen");
        triggerController.GetComponent<TriggerController>().ToggleTrigger();
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
