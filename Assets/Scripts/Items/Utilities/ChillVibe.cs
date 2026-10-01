using UnityEngine;

public class ChillVibe : Utility
{
    private float metaUpgradedRadius = 3f;
    private float metaUpgradedSlowPercent = 20f;

    private float inRunRadiusMultiplier = 1f;
    private float inRunSlowAdditive = 0f;

    private void Start()
    {
        InitializeStats();
    }

    public void InitializeStats()
    {
        if (utilityData == null) return;

        var radiusUpgrade = utilityData.GetUpgrade(StatType.Radius);
        metaUpgradedRadius = radiusUpgrade != null && MetaUpgradeManager.Instance != null
            ? MetaUpgradeManager.Instance.GetStatValue(radiusUpgrade)
            : 3f;

        var slowUpgrade = utilityData.GetUpgrade(StatType.SlowPercentage);
        metaUpgradedSlowPercent = slowUpgrade != null && MetaUpgradeManager.Instance != null
            ? MetaUpgradeManager.Instance.GetStatValue(slowUpgrade)
            : 20f;

        ApplyRadiusScale();
    }

    private void ApplyRadiusScale()
    {
        transform.localScale = Vector3.one * GetCurrentRadius();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && other.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.isSlow = true;
            ApplySlowToEnemy(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && other.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.isSlow = false;
            enemy.UpdateSpeed();
        }
    }

    private void ApplySlowToEnemy(Enemy enemy)
    {
        float slowFactor = Mathf.Clamp01(1f - (GetCurrentSlowPercent() / 100f));
        enemy.thisMovementSpeed = slowFactor * Enemy.movementSpeed * enemy.baseMovementSpeed;
    }

    public float GetCurrentRadius() => metaUpgradedRadius * inRunRadiusMultiplier;
    public float GetCurrentSlowPercent() => Mathf.Clamp(metaUpgradedSlowPercent + inRunSlowAdditive, 0f, 90f);
    public void IncreaseInRunRadius(float percentage)
    {
        inRunRadiusMultiplier *= (1f + (percentage / 100f));
        ApplyRadiusScale();
    }

    public void IncreaseInRunSlow(float percentagePoints)
    {
        inRunSlowAdditive += percentagePoints;
    }
}