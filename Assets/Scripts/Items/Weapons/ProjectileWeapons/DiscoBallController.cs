using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiscoBallController : ProjectileWeapon
{
    [SerializeField] private GameObject discoBallPrefab;

    private readonly List<DiscoBall> activeDiscoBalls = new List<DiscoBall>();

    public static DiscoBallController Instance { get; private set; }

    protected void Awake()
    {
        Instance = this;
    }

    protected override void Start()
    {
        base.Start();

        if (weaponData != null && weaponData.HasBeatTrigger)
        {
            UnityAction attackAction = new UnityAction(Attack);
            TriggerController.Instance.SetTrigger(weaponData.BeatNumber, attackAction);

            UnityAction blinkAction = new UnityAction(BlinkAllDiscoBalls);
            TriggerController.Instance.SetTrigger(1, blinkAction);
        }
    }

    public override void Attack()
    {
        if (gameObject.activeSelf && discoBallPrefab != null)
        {
            StartCoroutine(AttackDelayRoutine());
        }
    }

    private IEnumerator AttackDelayRoutine()
    {
        float bpm = SoundManager.Instance != null ? SoundManager.Instance.GetCurrentBPM() : 120f;
        float pitch = SoundManager.Instance != null ? SoundManager.Instance.transform.GetChild(0).GetComponent<AudioSource>().pitch : 1f;
        float burstDelay = ((60f / bpm) / 2f) / pitch;

        int count = GetCurrentProjectileCount();
        float damage = GetCurrentDamage();
        int penetration = GetCurrentPenetration();

        for (int i = 0; i < count; i++)
        {
            GameObject clone = Instantiate(discoBallPrefab, transform.position, Quaternion.identity);

            if (clone.TryGetComponent<DiscoBall>(out var ball))
            {
                ball.SetDamage(damage);
                ball.SetPenetration(penetration);
                activeDiscoBalls.Add(ball);
            }

            if (weaponData != null && weaponData.AttackSound != null)
            {
                float pitchShift = Mathf.Max(0.2f, 1f - (i * 0.05f));
                SoundManager.Instance.PlaySFX(weaponData.AttackSound, pitchShift);
            }

            yield return new WaitForSeconds(burstDelay);
        }
    }

    public void BlinkAllDiscoBalls()
    {
        for (int i = activeDiscoBalls.Count - 1; i >= 0; i--)
        {
            if (activeDiscoBalls[i] == null)
            {
                activeDiscoBalls.RemoveAt(i);
            }
            else
            {
                activeDiscoBalls[i].Blink();
            }
        }
    }

    public void UnregisterDiscoBall(DiscoBall ball)
    {
        activeDiscoBalls.Remove(ball);
    }
}