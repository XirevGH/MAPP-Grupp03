using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class VinylDiscController : ProjectileWeapon
{
    [SerializeField] private GameObject vinylDiscPrefab;

    public static VinylDiscController Instance { get; private set; }

    protected void Awake()
    {
        Instance = this;
    }

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
        if (gameObject.activeSelf && vinylDiscPrefab != null)
        {
            StartCoroutine(AttackDelayRoutine());
        }
    }

    private IEnumerator AttackDelayRoutine()
    {
        float bpm = SoundManager.Instance != null ? SoundManager.Instance.GetCurrentBPM() : 120f;
        float pitch = SoundManager.Instance != null ? SoundManager.Instance.transform.GetChild(0).GetComponent<AudioSource>().pitch : 1f;
        float attackDelay = ((60f / bpm) / 2f) / pitch;

        int projectileCount = GetCurrentProjectileCount();
        float currentDamage = GetCurrentDamage();
        int currentPenetration = GetCurrentPenetration();

        for (int i = 0; i < projectileCount; i++)
        {
            Vector3 spawnPos = player != null ? player.transform.position : transform.position;

            GameObject clone = Instantiate(vinylDiscPrefab, spawnPos, Quaternion.identity);

            if (clone.TryGetComponent<Projectile>(out var proj))
            {
                proj.SetDamage(currentDamage);
                proj.SetPenetration(currentPenetration);
            }

            if (clone.TryGetComponent<VinylDisc>(out var vinylScript))
            {
                vinylScript.isAtPlayer = true;
            }

            if (weaponData != null && weaponData.AttackSound != null)
            {
                float pitchShift = Mathf.Max(0.2f, 1f - (i * 0.05f));
                SoundManager.Instance.PlaySFX(weaponData.AttackSound, pitchShift);
            }

            yield return new WaitForSeconds(attackDelay);
        }
    }
}