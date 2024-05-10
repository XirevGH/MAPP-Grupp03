using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private int levelToload;
    [SerializeField] private GameObject exit;
    [SerializeField] private TextMeshProUGUI enemiesKilledText;

    
    public void LoadMainMenu()
    {
        MainManager.Instance.DestroyInstance();
        SoundManager.Instance.DestroyInstance();
        SceneManager.LoadScene(levelToload);
    }   
}
