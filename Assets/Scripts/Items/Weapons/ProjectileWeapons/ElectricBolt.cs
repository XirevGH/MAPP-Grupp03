using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ElectricBolt : MonoBehaviour
{
    [Header("Sprites & Visuals")]
    [SerializeField] private Sprite bolt1;
    [SerializeField] private Sprite bolt2;
    [SerializeField] private float spriteWidth = 10.24f;
    [SerializeField] private float spriteFlickerInterval = 0.05f;

    [Header("Lifetime")]
    [SerializeField] private float lifetime = 0.2f;

    private SpriteRenderer rend;
    private Light2D boltLight;

    private Transform sourcePlayer;
    private GameObject targetUnit;
    private float damage;

    private float lifetimeTimer;
    private float flickerTimer;
    private bool isLighter;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        boltLight = GetComponentInChildren<Light2D>();
    }


    public void Initialize(Transform playerTransform, GameObject target, float boltDamage)
    {
        sourcePlayer = playerTransform;
        targetUnit = target;
        damage = boltDamage;

        lifetimeTimer = lifetime;
        flickerTimer = spriteFlickerInterval;

        UpdateTransformPosition();
        DealDamage();
    }

    private void Update()
    {
        if (targetUnit == null || sourcePlayer == null)
        {
            Destroy(gameObject);
            return;
        }

        UpdateTransformPosition();

        lifetimeTimer -= Time.deltaTime;
        if (lifetimeTimer <= 0f)
        {
            Destroy(gameObject);
            return;
        }

        flickerTimer -= Time.deltaTime;
        if (flickerTimer <= 0f)
        {
            FlickerSpriteAndLight();
            flickerTimer = spriteFlickerInterval;
        }
    }

    private void UpdateTransformPosition()
    {
        Vector3 playerPos = sourcePlayer.position;
        Vector3 targetPos = targetUnit.transform.position;

        Vector3 direction = targetPos - playerPos;
        float length = direction.magnitude;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = playerPos;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.localScale = new Vector3(length / spriteWidth, transform.localScale.y, transform.localScale.z);
    }

    private void FlickerSpriteAndLight()
    {
        // Swap sprite
        rend.sprite = (rend.sprite == bolt1) ? bolt2 : bolt1;

        // Toggle light intensity
        isLighter = !isLighter;
        if (boltLight != null)
        {
            boltLight.intensity = isLighter ? 6f : 4f;
            boltLight.falloffIntensity = isLighter ? 0.3f : 0.5f;
        }
    }
    private void DealDamage()
    {
        if (targetUnit != null && targetUnit.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.TakeDamage(damage);
        }
    }
}
