using UnityEngine;

public static class GameBootstrapper
{
    // Runs automatically before ANY scene's Awake/Start methods!
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeServices()
    {
        // 1. Ensure SoundManager exists
        if (SoundManager.Instance == null)
        {
            GameObject soundPrefab = Resources.Load<GameObject>("SoundManager");
            if (soundPrefab != null)
            {
                GameObject soundObj = Object.Instantiate(soundPrefab);
                soundObj.name = "[SoundManager]";
            }
        }

        // 2. Ensure MetaUpgradeManager exists
        if (MetaUpgradeManager.Instance == null)
        {
            GameObject metaPrefab = Resources.Load<GameObject>("MetaUpgradeManager");
            if (metaPrefab != null)
            {
                GameObject metaObj = Object.Instantiate(metaPrefab);
                metaObj.name = "[MetaUpgradeManager]";
            }
        }
    }
}