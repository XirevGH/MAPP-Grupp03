using System.Collections.Generic;
using UnityEngine;

public class BoogieBomb : PhysicalWeapon
{
    private HashSet<Collider2D> colliders = new HashSet<Collider2D>();

    public bool usedAbility = false;
    public bool dealDamage = false;
    public bool bombMoving = false;
    public float abilityCooldown;
    public float bombRangeY;
    public float bombRangeX;

    [SerializeField] private GameObject bombParticles;
    [SerializeField] private Transform playerPosition;

    private void Start()
    {
        SetRandomBombRange();
    }

    private void FixedUpdate()
    {
        if (!bombMoving)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            transform.position = playerPosition.position;
        }
    }

    public void TriggerAbility()
    {
        if (!usedAbility)
        {
            bombMoving = true;
            transform.position = transform.position + new Vector3(bombRangeX, bombRangeY, 0);
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
        Invoke("AbilityCooldown", abilityCooldown);
    }

    private void TouchedGround()
    {
        Instantiate(bombParticles, transform.position, bombParticles.transform.localRotation);
        this.GetComponent<SpriteRenderer>().enabled = true;
        dealDamage = true;
        Invoke("ReturnBomb", 1f);
    }

    private void ReturnBomb()
    {
        SetRandomBombRange();
        bombMoving = false;
    }

    private void AbilityCooldown()
    {
        usedAbility = false;
    }

    private void SetRandomBombRange()
    {
        bombRangeY = Random.Range(-5, 5);
        bombRangeX = Random.Range(-5, 5);
    }
}
