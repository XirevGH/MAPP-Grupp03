using UnityEngine;

public class SynthwaveBolt : Projectile
{
    [Header("Movement & Growth Settings")]
    [SerializeField] private float growSpeed = 10f;       // Speed at which bolt extends
    [SerializeField] private float travelSpeed = 25f;     // Speed at which bolt shoots outward
    [SerializeField] private float maxLifetime = 1.5f;    // Destroy after this many seconds if it hits nothing

    private float currentLifetime;
    private float targetScaleX = 1f;

    private void Awake()
    {
        // Start squished (0 length on X)
        transform.localScale = new Vector3(0f, transform.localScale.y, transform.localScale.z);
        currentLifetime = maxLifetime;
    }

    private void Update()
    {
        if (transform.localScale.x < targetScaleX)
        {
            float newScaleX = Mathf.MoveTowards(transform.localScale.x, targetScaleX, growSpeed * Time.deltaTime);
            transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localPosition += new Vector3(travelSpeed * Time.deltaTime, 0f, 0f);
        }

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

    protected override void Despawn()
    {
        if (transform.parent != null && transform.parent.name.Contains("Pivot"))
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            base.Despawn();
        }
    }

}
