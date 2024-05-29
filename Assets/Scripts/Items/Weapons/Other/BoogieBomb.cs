using System.Collections.Generic;
using UnityEngine;

public class BoogieBomb : PhysicalWeapon
{
    private HashSet<Collider2D> colliders = new HashSet<Collider2D>();
    private AudioSource audioSource;

    public bool usedAbility = false;
    public bool dealDamage = false;
    public bool bombMoving = false;
    public float abilityCooldown; // Assuming this is set in the Inspector or elsewhere
    public float bombRangeY;
    public float bombRangeX;

    public AudioClip bombClip;
    [SerializeField] private GameObject bombParticles;
    [SerializeField] private Transform playerPosition;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ResetBombPosition();
    }

    void FixedUpdate()
    {
        if (!bombMoving)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            transform.position = playerPosition.position;
        }

        if (!usedAbility)
        {
            bombMoving = true;
            transform.position += new Vector3(bombRangeX, bombRangeY, 0);
            Attack();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (usedAbility && !colliders.Contains(other) && other.gameObject.CompareTag("Enemy"))
        {
            colliders.Add(other);
            DealDamage(other);
        }

        if (!dealDamage)
        {
            colliders.Clear();
        }
    }

    public override void Attack()
    {
        usedAbility = true;

        Invoke("TouchedGround", 0.5f);
        Invoke("ResetAbility", abilityCooldown);
    }

    private void TouchedGround()
    {
        audioSource.PlayOneShot(bombClip, 0.7f);
        Instantiate(bombParticles, transform.position, bombParticles.transform.localRotation);
        GetComponent<SpriteRenderer>().enabled = true;
        dealDamage = true;
        Invoke("ReturnBomb", 1f);
    }

    private void ReturnBomb()
    {
        ResetBombPosition();
        bombMoving = false;
    }

    private void ResetBombPosition()
    {
        bombRangeY = Random.Range(-5, 5);
        bombRangeX = Random.Range(-5, 5);
    }

    private void ResetAbility()
    {
        usedAbility = false;
    }
}
