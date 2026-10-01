using UnityEngine;
using UnityEngine.Events;

public class SynthwaveBlast : ProjectileWeapon
{
    [SerializeField] GameObject synthwavePivotPrefab;

    protected override void Start()
    {
        base.Start();

        if (weaponData != null && weaponData.HasBeatTrigger)
        {
            UnityAction action = new UnityAction(Attack);
            TriggerController.Instance.SetTrigger(weaponData.BeatNumber, action);
        }
    }

    public override void Attack()
    {
        if (!gameObject.activeSelf || synthwavePivotPrefab == null) return;

        if (weaponData != null && weaponData.AttackSound != null)
        {
            SoundManager.Instance.PlaySFX(weaponData.AttackSound, 1f);
        }

        int projectileCount = GetCurrentProjectileCount();
        float finalDamage = GetCurrentDamage();
        int finalPenetration = GetCurrentPenetration();

        for (int i = 0; i < projectileCount; i++)
        {
            float randomAngle = Random.Range(0f, 360f);

            GameObject clone = Instantiate(synthwavePivotPrefab, transform.position, Quaternion.Euler(0f, 0f, randomAngle), transform);

            if (clone.TryGetComponent<SynthwaveBolt>(out var bolt) ||
                (clone.transform.childCount > 0 && clone.transform.GetChild(0).TryGetComponent(out bolt)))
            {
                bolt.SetDamage(finalDamage);
                bolt.SetPenetration(finalPenetration);
            }
        }
    }
}
