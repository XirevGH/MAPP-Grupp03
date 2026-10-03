using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    [SerializeField] private SceneTransition transition;
    public GameObject pursePanel;
    public GameObject canvasWorldSpace;
    public PlayerStats playerStats;
    public Camera mainCamera;
    public Tilemap tilemap;

    public int currentTrackBPM;

    private string playerStatsFile;

    private void Awake()
    {
        playerStatsFile = Path.Combine(Application.persistentDataPath, "playerInfo.json");
        ReadFile(playerStatsFile);

        if (mainCamera == null) mainCamera = Camera.main;

        Enemy.movementSpeed = 1f;
        Enemy.healthProcenIncrease = 1f;
    }

    private void Start()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.StartGame();
        }
    }

    private void Update()
    {
        if (SoundManager.Instance != null)
        {
            currentTrackBPM = SoundManager.Instance.GetCurrentBPM();
        }
    }

    private void FixedUpdate()
    {
        Enemy.movementSpeed += 0.0001f;
    }

    private void ReadFile(string saveFile)
    {
        if (playerStats == null) playerStats = FindObjectOfType<PlayerStats>();

        if (File.Exists(saveFile) && playerStats != null)
        {
            string fileContents = File.ReadAllText(saveFile);
            playerStats.CreateFromJSON(fileContents);
        }
    }

    public void GameOver()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Die();
        }

        if (playerStats != null)
        {
            File.WriteAllText(playerStatsFile, playerStats.SaveToString());
        }

        if (transition != null)
        {
            transition.ChangeScene();
        }
    }

    public BoundsInt GetBoundsFromCamera()
    {
        float cameraSize = mainCamera.orthographicSize;
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3Int minPosition = tilemap.WorldToCell(cameraPosition - new Vector3(cameraSize - 2 * mainCamera.aspect * 2.5f, cameraSize + 10, 0));
        Vector3Int maxPosition = tilemap.WorldToCell(cameraPosition + new Vector3(cameraSize - 5 * mainCamera.aspect, cameraSize + 10, 0));
        return new BoundsInt(minPosition, maxPosition - minPosition);
    }
}