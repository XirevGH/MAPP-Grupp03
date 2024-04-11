using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float movementSpeed;

    public float health;
    public TMP_Text damageNumbers;
    public Animator damageNumberAnim;
    public Animator enemyAnim;
    private Rigidbody2D rb; 
    private SpriteRenderer sprite;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (IsAlive()) 
        {
            if (transform.position.x < player.GetComponent<Transform>().position.x)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
            enemyAnim.SetTrigger("Walking");
            transform.position = Vector3.MoveTowards(transform.position, player.GetComponent<Transform>().position, movementSpeed/200);
        }
    }

    public void TakeDamage(float damageTaken)
    {
        if (IsAlive()) { 
            health -= damageTaken;
            damageNumbers.text = damageTaken.ToString();
            damageNumberAnim.SetTrigger("TakingDamage");
            enemyAnim.SetTrigger("TakeDamage");

        }

    }

    private bool IsAlive()
    {
        if (health <= 0)
        {
            enemyAnim.SetTrigger("Dead");
            Invoke("DestroyGameObject", 0.8f);
            return false;
        }
        return true;

    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void OnParticleCollision(GameObject particle)
    {
        TakeDamage(Random.Range(1, 4));
    }
}
