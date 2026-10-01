using UnityEngine;

public class RollerSkates : Utility
{
    [SerializeField] private PlayerMovement playerMovement;

    private bool isBonusApplied = false;

    private void Start()
    {
        ApplyMetaSpeedBonus();
    }

    private void ApplyMetaSpeedBonus()
    {
        if (isBonusApplied) return;

        if (playerMovement == null)
        {
            if (player != null)
            {
                playerMovement = player.GetComponent<PlayerMovement>();
            }
            else
            {
                playerMovement = FindObjectOfType<PlayerMovement>();
            }
        }

        if (utilityData == null || playerMovement == null) return;

        var speedUpgrade = utilityData.GetUpgrade(StatType.MovementSpeed);

        if (speedUpgrade != null && MetaUpgradeManager.Instance != null)
        {
            float totalSpeedBonusPercent = MetaUpgradeManager.Instance.GetStatValue(speedUpgrade);

            if (totalSpeedBonusPercent > 0f)
            {
                playerMovement.IncreaseMovementSpeed(1f + (totalSpeedBonusPercent / 100f));
            }
        }

        isBonusApplied = true;
    }

    public void IncreaseInRunMovementSpeed(float percentage)
    {
        if (playerMovement != null)
        {
            playerMovement.IncreaseMovementSpeed(1f + (percentage / 100f));
        }
    }
}