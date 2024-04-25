using System;
using System.Collections.Generic;
using UnityEngine;

public class StaticUpdateManager : MonoBehaviour
{
    private static StaticUpdateManager instance;
    private List<Action> updateActions = new List<Action>();
    private float updateInterval = 0.4f;  // Interval between updates 0.4f = 0.4s
    private float nextUpdateTime = 0.0f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Time.time >= nextUpdateTime)
        {
            foreach (var action in updateActions)
            {
                action.Invoke();
            }
            nextUpdateTime = Time.time + updateInterval;
        }
    }

    public static void RegisterUpdate(Action action)
    {
        if (instance != null && !instance.updateActions.Contains(action))
        {
            instance.updateActions.Add(action);
        }
    }

    public static void UnregisterUpdate(Action action)
    {
        if (instance != null)
        {
            instance.updateActions.Remove(action);
        }
    }
}
