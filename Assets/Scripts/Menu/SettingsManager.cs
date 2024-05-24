using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private bool isSetOnRight = false;

    public static SettingsManager Instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool GetIsSetOnRight()
    {
        return isSetOnRight;
    }

    public void ChangePauseButtonSides(bool isSetOnRight)
    {
        this.isSetOnRight = isSetOnRight;
        Debug.Log("IsSetOnRight is: " + isSetOnRight);
    }
}
