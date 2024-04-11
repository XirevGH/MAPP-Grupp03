using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameObjectComparer : IComparer<GameObject>
{
    public int Compare(GameObject obj1, GameObject obj2)
    {
        if (Vector3.Distance(obj1.transform.position, obj1.GetComponent<Enemy>().player.transform.position) < Vector3.Distance(obj2.transform.position, obj1.GetComponent<Enemy>().player.transform.position))
        {
            return -1;
        }
        else if (Vector3.Distance(obj1.transform.position, obj1.GetComponent<Enemy>().player.transform.position) > Vector3.Distance(obj2.transform.position, obj1.GetComponent<Enemy>().player.transform.position))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}

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

    public float GetHealth() 
    {
        return health; 
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
        TakeDamage(UnityEngine.Random.Range(1, 4));
    }
}
