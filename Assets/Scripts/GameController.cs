using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    private string saveFile;
    public GameObject player;
    private GameObject playerFromSave;
    public Camera mainCamera;
    public Tilemap tilemap;

    private void Awake()
    {
        saveFile = Application.persistentDataPath + "/playerInfo.json";
        ReadFile();
        mainCamera = Camera.main;
        Enemy.movementSpeed = 4f;
        Debug.Log("Start");
    }

    private void FixedUpdate()
    {
        Enemy.movementSpeed += 0.001f;
    }
    private void ReadFile()
    {
        if (File.Exists(saveFile))
        {
            string fileContents = File.ReadAllText(saveFile);
            //playerFromSave = Player.CreateFromJSON(fileContents);
            //playerPrefab = Instantiate(playerPrefab, new Vector3(0f, 0f), Quaternion.identity);
            //playerPrefab = playerFromSave;
        }
        else
        {
            Debug.Log("File does not exist.");
        }
    }

    public void GameOver()
    {
        //File.WriteAllText(saveFile, playerPrefab.GetComponent<Player>().SaveToString());
        SceneManager.LoadScene("MainMenu");
    }

    public BoundsInt GetBoundsFromCamera()
    {
        float cameraSize = mainCamera.orthographicSize;
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3Int minPosition = tilemap.WorldToCell(cameraPosition - new Vector3(cameraSize * mainCamera.aspect, cameraSize, 0));
        Vector3Int maxPosition = tilemap.WorldToCell(cameraPosition + new Vector3(cameraSize * mainCamera.aspect, cameraSize, 0));
        return new BoundsInt(minPosition, maxPosition - minPosition);
    }
}
