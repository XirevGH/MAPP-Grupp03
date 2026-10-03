using UnityEngine;

public class NoteProjectile : Projectile
{
    [Header("Visuals & Movement")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    [SerializeField] private float maxLifetime = 5f;

    private Vector3 direction;
    private float currentLifetime;

    private void Awake()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        currentLifetime = maxLifetime;
    }

    public void Initialize(float damage, float speed, int penetration, Vector3 direction)
    {
        this.damage = damage;
        this.speed = speed;
        this.penetration = penetration;
        this.direction = direction;

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = Random.Range(0, 2) == 1 ? sprite1 : sprite2;
        }
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        currentLifetime -= Time.deltaTime;
        if (currentLifetime <= 0f)
        {
            Despawn();
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
}