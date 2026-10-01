using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class YoyoController : PermanentProjectileWeapon
{
    [SerializeField] private GameObject yoyoPrefab;

    private readonly List<Yoyo> activeYoyos = new List<Yoyo>();

    public static YoyoController Instance { get; private set; }

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

        SynchronizeYoyoCount();
    }

    private void Update()
    {
        if (GetCurrentProjectileCount() > activeYoyos.Count)
        {
            SynchronizeYoyoCount();
        }
    }

    public override void Attack()
    {
        if (!gameObject.activeSelf || activeYoyos.Count == 0) return;

        if (weaponData != null && weaponData.AttackSound != null)
        {
            SoundManager.Instance.PlaySFX(weaponData.AttackSound, 1f);
        }

        for (int i = 0; i < activeYoyos.Count; i++)
        {
            if (activeYoyos[i] != null)
            {
                activeYoyos[i].ActivateSuperMode();
            }
        }
    }

    private void SynchronizeYoyoCount()
    {
        int targetCount = GetCurrentProjectileCount();

        while (activeYoyos.Count < targetCount)
        {
            SpawnSingleYoyo();
        }

        RecalculateYoyoSpacing();
    }

    private void RecalculateYoyoSpacing()
    {
        int total = activeYoyos.Count;
        if (total == 0) return;

        float angleStep = 360f / total;
        float currentAngle = 0f;

        for (int i = 0; i < total; i++)
        {
            if (activeYoyos[i] != null)
            {
                activeYoyos[i].angle = currentAngle;
                activeYoyos[i].ResetSuperMode();
                activeYoyos[i].SetDamage(GetCurrentDamage());
                currentAngle += angleStep;
            }
        }
    }

    private void SpawnSingleYoyo()
    {
        if (yoyoPrefab == null) return;

        GameObject clone = Instantiate(yoyoPrefab, transform.position, Quaternion.identity, transform);

        if (clone.TryGetComponent<Yoyo>(out var yoyoScript))
        {
            yoyoScript.SetDamage(GetCurrentDamage());
            activeYoyos.Add(yoyoScript);
        }
    }
    public override void IncreaseInRunDamage(float percentIncrease)
    {
        base.IncreaseInRunDamage(percentIncrease);

        float newDamage = GetCurrentDamage();
        for (int i = 0; i < activeYoyos.Count; i++)
        {
            if (activeYoyos[i] != null)
            {
                activeYoyos[i].SetDamage(newDamage);
            }
        }
    }
}