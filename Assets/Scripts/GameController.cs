using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private string saveFile;
    public Player playerPrefab;
    private Player playerFromSave;

    private void Awake()
    {
        string saveFile = Application.persistentDataPath + "/playerInfo.json";
        ReadFile();
    }

    private void ReadFile()
    {
        if (File.Exists(saveFile))
        {
            string fileContents = File.ReadAllText(saveFile);
            playerFromSave = Player.CreateFromJSON(fileContents);
            playerPrefab = Instantiate(playerPrefab, new Vector3(0f, 0f), Quaternion.identity);
            playerPrefab = playerFromSave;
        }
        else
        {
            Debug.Log("File does not exist.");
        }
    }

    public void GameOver()
    {
        File.WriteAllText(saveFile, playerPrefab.GetComponent<Player>().SaveToString());
    }
}
