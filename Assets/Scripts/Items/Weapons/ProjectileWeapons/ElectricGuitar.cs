using UnityEngine;
using UnityEngine.Events;

public class ElectricGuitar : TetheringWeapon
{
    [SerializeField] private GameObject bolt;

    protected override void Start()
    {
        base.Start();

        if (weaponData != null && weaponData.HasBeatTrigger)
        {
            UnityAction action = new UnityAction(Attack);
            TriggerController.Instance.SetTrigger(weaponData.BeatNumber, action);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
        }
    }

    public override void Attack()
    {
        if (!gameObject.activeSelf) return;

        // Clean up dead enemies killed by other weapons
        enemies.RemoveWhere(e => e == null);

        if (enemies.Count > 0)
        {
            if (weaponData != null && weaponData.AttackSound != null)
            {
                SoundManager.Instance.PlaySFX(weaponData.AttackSound, 1f);
            }

            GameObject[] targetEnemies = GetClosestEnemies(AdjustTargetOverflow(GetCurrentTetherAmount()));
            float finalDamage = GetCurrentDamage();
            Transform sourceTransform = player != null ? player.transform : transform;

            for (int i = 0; i < targetEnemies.Length; i++)
            {
                if (targetEnemies[i] != null && bolt != null)
                {
                    GameObject clone = Instantiate(bolt);

                    if (clone.TryGetComponent<ElectricBolt>(out var electricBolt))
                    {
                        electricBolt.Initialize(sourceTransform, targetEnemies[i], finalDamage);
                    }
                }
            }
        }
    }
}