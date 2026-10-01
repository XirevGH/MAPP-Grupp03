using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Header("Base Projectile Stats")]
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected int penetration = 1;

    public float Damage => damage;
    public float Speed => speed;
    public int Penetration => penetration;

    public virtual void Initialize(float damage, float speed, int penetration)
    {
        this.damage = damage;
        this.speed = speed;
        this.penetration = Mathf.Max(1, penetration);
    }

    public void SetDamage(float amount) => damage = amount;
    public void SetSpeed(float amount) => speed = amount;
    public void SetPenetration(int amount) => penetration = Mathf.Max(1, amount);

    public virtual void DealDamage(Collider2D other)
    {
        if (other != null && other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    public virtual void DestroyWhenMaxPenetration()
    {
        penetration--;

        if (penetration <= 0)
        {
            Despawn();
        }
    }

    protected virtual void Despawn()
    {
        Destroy(gameObject);
    }
}