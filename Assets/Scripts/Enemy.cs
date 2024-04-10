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
    private bool isDead;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.GetComponent<Transform>().position, movementSpeed/200);
        CheckIfDead();
    }

    public void TakeDamage(float damageTaken)
    {
        if (!isDead) { 
            health -= damageTaken;
            damageNumbers.text = damageTaken.ToString();
            damageNumberAnim.SetTrigger("TakingDamage");
            enemyAnim.SetTrigger("TakeDamage");
        }

    }

    private void CheckIfDead()
    {
        if (health <= 0 && !isDead)
        {
            isDead = true;
            enemyAnim.SetTrigger("Dead");
            Invoke("DestroyGameObject", 0.8f);
        }
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
