using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DecoyController : Utility
{

    [Header("Decoy Prefab & Throw Settings")]
    [SerializeField] private GameObject decoyPrefab;
    [SerializeField] private float throwDistance = 5f;
    [SerializeField] private float throwDelayTime = 0.15f;


    private int metaUpgradedDecoyAmount;
    private float metaUpgradedDecoyHealth;

    private int inRunBonusDecoyAmount = 0;
    private float inRunHealthMultiplier = 1f;

    public DynamicJoystick dynamicJoystick;
    private Vector2 playerDirection;
    private Vector2 throwingDirection;
    private Vector2 playerPosition;

    private void Start()
    {
        InitializeStats();

        if (utilityData != null && utilityData.HasBeatTrigger)
        {
            UnityAction action = new UnityAction(Throw);
            TriggerController.Instance.SetTrigger(utilityData.BeatNumber, action);
        }
    }

    public void InitializeStats()
    {
        if (utilityData == null) return;

        var amountUpgrade = utilityData.GetUpgrade(StatType.DecoyAmount);
        metaUpgradedDecoyAmount = amountUpgrade != null && MetaUpgradeManager.Instance != null
            ? Mathf.RoundToInt(MetaUpgradeManager.Instance.GetStatValue(amountUpgrade))
            : 1;

        var healthUpgrade = utilityData.GetUpgrade(StatType.DecoyHealth);
        metaUpgradedDecoyHealth = healthUpgrade != null && MetaUpgradeManager.Instance != null
            ? MetaUpgradeManager.Instance.GetStatValue(healthUpgrade)
            : 10f;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main") {
            if (dynamicJoystick == null)
            {
                DynamicJoystick[] dynamicJoysticks = FindObjectsOfType<DynamicJoystick>();
                foreach (DynamicJoystick joystick in dynamicJoysticks)
                {
                    if (joystick.GetPosition() == "Right")
                    {
                        dynamicJoystick = joystick;
                    }
                }
            }
            if (player != null && dynamicJoystick != null)
            {
                playerPosition = player.transform.position;
                playerDirection = new Vector2(dynamicJoystick.Horizontal, dynamicJoystick.Vertical).normalized;
            }
        }
    }

    public void Throw()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(ThrowDelayRoutine());
        }
    }

    private IEnumerator ThrowDelayRoutine()
    {
        int totalDecoys = GetCurrentDecoyAmount();
        float currentHealth = GetCurrentDecoyHealth();

        for (int i = 0; i < totalDecoys; i++)
        {
            GameObject newDecoy = Instantiate(decoyPrefab, transform.position, Quaternion.identity);

            if (newDecoy.TryGetComponent<Decoy>(out var decoyComponent))
            {
                decoyComponent.endPosition = FindLandingSpot();
                decoyComponent.SetHealth(currentHealth);
            }

            yield return new WaitForSeconds(throwDelayTime);
        }
    }

    private Vector2 FindLandingSpot()
    {
        throwingDirection = playerDirection == Vector2.zero ? Vector2.down : -playerDirection;
        return playerPosition + (throwDistance * throwingDirection);
    }

    public int GetCurrentDecoyAmount() => metaUpgradedDecoyAmount + inRunBonusDecoyAmount;
    public float GetCurrentDecoyHealth() => metaUpgradedDecoyHealth * inRunHealthMultiplier;

    public void IncreaseInRunDecoyAmount(int amount) => inRunBonusDecoyAmount += amount;
    public void IncreaseInRunDecoyHealth(float percentage) => inRunHealthMultiplier *= (1f + (percentage / 100f));
}
