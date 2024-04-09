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
    public Animator anim;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(this.GetComponent<Transform>().position, player.GetComponent<Transform>().position, movementSpeed/200);

        CheckIfDead();
    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
        damageNumbers.text = damageTaken.ToString();
        anim.SetTrigger("TakingDamage");
    }

    private void CheckIfDead()
    {
        Debug.Log(health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
