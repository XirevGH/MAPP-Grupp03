using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Player playerPrefab;
    private Player playerFromSave;

    private void Start()
    {
        string saveFile = Application.persistentDataPath + "/playerInfo.json";
        playerFromSave = Player.CreateFromJSON(saveFile);
        playerPrefab = Instantiate(playerPrefab, new Vector3(0f, 0f), Quaternion.identity);
        playerPrefab = playerFromSave;
    }

    public void GameOver()
    {
        playerPrefab.GetComponent<Player>().SaveToString();
    }
    
}
