using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiscoBall : Projectile
{
    [Header("Launch Physics")]
    [SerializeField] private float launchHeight = 8f;
    [SerializeField] private float launchDistance = 4f;
    [SerializeField] private float launchTorque = 10f;
    [SerializeField] private float maxLifetime = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float lifetimeTimer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        lifetimeTimer = maxLifetime;
    }

    private void Start()
    {
        float randomX = Random.Range(-launchDistance, launchDistance);
        Vector2 launchForce = new Vector2(randomX, launchHeight - Mathf.Abs(randomX));

        if (rb != null)
        {
            rb.AddForce(launchForce, ForceMode2D.Impulse);
            rb.AddTorque(launchTorque * Mathf.Sign(randomX), ForceMode2D.Impulse);
        }

        Blink();
    }

    private void Update()
    {
        lifetimeTimer -= Time.deltaTime;
        if (lifetimeTimer <= 0f)
        {
            Despawn();
        }
    }

    public void Blink()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.HSVToRGB(Random.Range(0f, 1f), 0.75f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            DealDamage(other);
            DestroyWhenMaxPenetration();
        }
    }

    protected override void Despawn()
    {
        if (DiscoBallController.Instance != null)
        {
            DiscoBallController.Instance.UnregisterDiscoBall(this);
        }

        base.Despawn();
    }
}