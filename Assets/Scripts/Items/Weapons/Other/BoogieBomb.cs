using System.Collections.Generic;
using UnityEngine;

public class BoogieBomb : PhysicalWeapon
{
    HashSet<Collider2D> colliders = new HashSet<Collider2D>();

    public bool usedAbility = false;
    public bool dealDamage = false;
    public bool bombMoving = false;
    public float abilityCooldown; //60f / (gameController.GetComponent<GameController>().GetCurrentTrackBPM() / TriggerController.GetComponent<TriggerController>().GetTrigger("den triggern som triggar objectet").noteValue);
    public float bombRangeY;
    public float bombRangeX;


    [SerializeField] private GameObject bombParticles;

    [SerializeField] private Transform playerPosition;


    private void Start()
    {
     bombRangeY = UnityEngine.Random.Range(-5, 5);
     bombRangeX = UnityEngine.Random.Range(-5, 5);
    }

    void FixedUpdate()
    {
        if (!bombMoving)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            transform.position = playerPosition.position;
        }


        if (!usedAbility)
        {
            bombMoving = true;
            transform.position = transform.position + new UnityEngine.Vector3(bombRangeX, bombRangeY, 0);
            Attack();

            
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {

        if (usedAbility && !colliders.Contains(other) && other.gameObject.CompareTag("Enemy")) //checkar om det finns enemies inom collidern som kan göras skada på
        {
            colliders.Add(other);
            DealDamage(other);
        }
        if (!dealDamage)
        {
            colliders.Clear();
        }
    }


    override public void Attack() //Gör så att man använder abilityn
    {
        //TODO lägg till animation
        usedAbility = true;
        Invoke("TouchedGround", 0.5f); //gör så att abilityn gör damage och visar att abilityn har använts
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
        bombRangeY = UnityEngine.Random.Range(-5, 5);
        bombRangeX = UnityEngine.Random.Range(-5, 5);
        bombMoving = false;
    }

    private void AbilityCooldown()
    {
        usedAbility = false;
    }

    public override int GetIncreaseDamageCost()
    {
        return damageUpgradeCost;
    }
}
