using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
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

    [SerializeField] private GameObject xpMagnetPrefab;
    [SerializeField] private GameObject xpDropPrefab;
    public static float movementSpeed;
    public float health;
    public TMP_Text damageNumbers;
    public Animator damageNumberAnim;
    public Animator enemyAnim;
    private Rigidbody2D rb; 
    private SpriteRenderer sprite;
    [SerializeField] private Sprite bouncerSprite, dancerSprite;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            damageNumberAnim.SetTrigger("TakingDamage");
            enemyAnim.SetTrigger("TakeDamage");
        }
    }

    public string Repeat(string text, uint n)
    {
        var textAsSpan = text.AsSpan();
        var span = new Span<char>(new char[textAsSpan.Length * (int)n]);
        for (var i = 0; i < n; i++)
        {
            textAsSpan.CopyTo(span.Slice((int)i * textAsSpan.Length, textAsSpan.Length));
        }

        return span.ToString();
    }
    private string BuildDamageNumber(float damage)
    {
        string source = damageNumbers.text;
        int count = source.Length - source.Replace("/", "").Length;
        StringBuilder builder = new StringBuilder(damageNumbers.text);
        if (count > 0) { 
            builder.Insert(0, Repeat(" ", (uint)count * 16));
            builder.Insert(1, damage);
            builder.Insert(2, "\n");
        }
        else
        {
            builder.Insert(0, damage + "\n");
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
            if (xpDropPrefab) {
                GameObject xpDrop = Instantiate(xpDropPrefab, transform.position, Quaternion.identity);
            xpDrop.SetActive(true);
            }
        }else if(xpMagnetPrefab){
            int random2 = GetRandomInt(1,9);
            if(random2 == 1){
                GameObject xpMagnet = Instantiate(xpMagnetPrefab, transform.position, Quaternion.identity);
            xpMagnet.SetActive(true);
            }
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
