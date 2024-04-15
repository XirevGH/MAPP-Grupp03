using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using System;
using System.Linq;

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
    public GameObject xpDrop;
    public static float movementSpeed;
    public float health;
    public TMP_Text damageNumbers;
    public Animator damageNumberAnim;
    public Animator enemyAnim;
    private Rigidbody2D rb; 
    private SpriteRenderer sprite;
    [SerializeField] private Sprite bouncerSprite, dancerSprite;
    private float damageNumberWindow = 3f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        xpDrop = GameObject.FindGameObjectWithTag("XPDrop20");
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        int random = GetRandomInt(1, 3);
        if (random == 1) {
            sprite.sprite = bouncerSprite;
        } else
        {
            sprite.sprite = dancerSprite;
        }
        
    }

    void FixedUpdate()
    {
        damageNumberWindow -= Time.deltaTime;
        if (IsAlive()) 
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 0.5)
            {
                player.GetComponent<Player>().TakeDamage(1);
            }
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
            damageNumbers.text = BuildDamageNumber(damageTaken);
            if (damageNumberWindow <= 0)
            {
                damageNumberWindow = 3f;
                damageNumberAnim.SetTrigger("TakingDamage");
            }
            enemyAnim.SetTrigger("TakeDamage");
            
        }
    }


    private string BuildDamageNumber(float damage)
    {
        string source = damageNumbers.text;
        int count = source.Split('\n').Length;
        if (count > 5 || damageNumberWindow <= 0)
        {
            damageNumbers.text = "";
        }
        StringBuilder builder = new StringBuilder(damageNumbers.text);
        if (count > 0)
        {
            
            builder.Insert(0, damage.ToString());
            builder.Insert(0, " ", count);
            builder.Insert(0, "\n");

        }
        else
        {
            builder.Append(damage.ToString());
        }

        return builder.ToString();
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

    private void DropXP()
    {
        int random = UnityEngine.Random.Range(1, 3);
        if(random == 1)
        {
            GameObject clone = Instantiate(xpDrop, transform.position, Quaternion.identity);
        }
    }

    private void DestroyGameObject()
    {
        DropXP();
        Destroy(gameObject);
    }

    private void OnParticleCollision(GameObject particle)
    {
        TakeDamage(UnityEngine.Random.Range(1, 4));
    }

    private int GetRandomInt (int a, int b)
    {
        return UnityEngine.Random.Range(a, b);
}
}
