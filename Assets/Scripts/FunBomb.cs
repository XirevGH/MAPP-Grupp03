using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FunBomb : PhysicalWeapon
{
    private HashSet<Collider2D> colliders = new HashSet<Collider2D>();

    public bool usedAbility = false;
    public bool dealDamage = false;
    public bool bombMoving = false;
    public float abilityCooldown;
    public float bombRangeY;
    public float bombRangeX;

    private AudioSource audioSource;

    [SerializeField] private GameObject bombParticles;
    [SerializeField] private Transform playerPosition;

    private void Start()
    {
     //   UnityAction action = new UnityAction(TriggerAbility);
        SetRandomBombRange();
   //     TriggerController.Instance.SetTrigger(4, action);
        Debug.Log("Program starting");
    }


    private void FixedUpdate()
    {
        if (!bombMoving)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            transform.position = playerPosition.position;
        }
    }

    public void TriggerAbility()
    {
        if (!usedAbility)
        {
            bombMoving = true;
            transform.position = transform.position + new Vector3(bombRangeX, bombRangeY, 0);


            Invoke("TouchedGround", 0.5f);
            Invoke("AbilityCooldown", abilityCooldown);
            usedAbility = true;
            Debug.Log("Main use done");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (usedAbility && !colliders.Contains(other) && other.gameObject.CompareTag("Enemy"))
        {
            colliders.Add(other);
            DealDamage(other);
            Debug.Log("Deals damage");
        }
        if (!dealDamage)
        {
            colliders.Clear();
        }
    }


    public override void Attack()
    {
        /*  usedAbility = true;
          Invoke("TouchedGround", 0.5f);
          Invoke("AbilityCooldown", abilityCooldown); */
    }

    private void TouchedGround()
    {
        audioSource.Play();
        Instantiate(bombParticles, transform.position, bombParticles.transform.localRotation);
        Debug.Log("instantiated but not rendered");
        GetComponent<SpriteRenderer>().enabled = true;
        dealDamage = true;
        Invoke("ReturnBomb", 1f);
        Debug.Log("Touched ground and rendered");
    }

    private void ReturnBomb()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        SetRandomBombRange();
        bombMoving = false;
        Debug.Log("Bomb safely returned");
    }

    private void AbilityCooldown()
    {
        usedAbility = false;
        Debug.Log("UsedAbility false");
    }

    private void SetRandomBombRange()
    {
    //    bombRangeY = Random.Range(-3, 3);
      //  bombRangeX = Random.Range(-3, 3);
    }
}
