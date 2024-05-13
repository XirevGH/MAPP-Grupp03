using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using System;
using Unity.VisualScripting;

[System.Serializable]  
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
[System.Serializable]  
public class DropItem
{
    //Prefaben som ska skapas
    [SerializeField] public GameObject item;

    //chansen i int "%" hur stor changen är att den droppas
    
    [Range(0,100)] public float dropChance;
}
public class Enemy : MonoBehaviour
{

    [SerializeField] private Sprite enemySprite;
    
    [SerializeField] private List<DropItem> drops = new List<DropItem>();
    //om den kan droppa ferla saker än en. Börja med först droppet i listan

    [SerializeField] int xpValue;
    public bool multiDrop;
    
    public GameObject player, target;
    protected float damageNumberWindow = 1f;
    public SpriteRenderer sprite;
    public static float movementSpeed;  // Global % enemy movespeed increase.  
    public static float healthProcenIncrease;
    public float health;
     
    public float thisMovementSpeed; 

    public float baseMovementSpeed;
    public bool isSlow;

    protected float startingHealth;

    public TMP_Text damageNumbers;
    public Animator damageNumberAnim;
    public Animator enemyAnim;
    public AudioClip hitSound;

    protected virtual void Start()
    {   startingHealth = health * healthProcenIncrease;
        health = startingHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        sprite = GetComponent<SpriteRenderer>();
        UpdateSpeed();
        isSlow = false;
        target = player;
        StaticUpdateManager.RegisterUpdate(CustomSlowUpdate);
    }

    protected virtual void CustomSlowUpdate() //Slow uppdate 0.4s
    {
        if (!isSlow)
        {
            UpdateSpeed();

        }

    }

    void FixedUpdate()
    {
        SelectTarget();
        damageNumberWindow -= Time.deltaTime;


        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            StartCoroutine(RemoveForce(rb, 1f));
        }


        if (IsAlive()) 
        {
            if (Vector3.Distance(target.transform.position, transform.position) < 0.5)
            {
                player.GetComponent<Player>().TakeDamage(1);
            }

            if (Vector3.Distance(player.transform.position, transform.position) < 1)
            {
                player.GetComponent<Player>().TakeDamage(1);
            }

            if (transform.position.x < target.transform.position.x)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }

            enemyAnim.SetTrigger("Walking");
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, thisMovementSpeed / 200);
        }
           
    }

    protected void SelectTarget(){
         if(GameObject.FindGameObjectWithTag("DecoyObject") != null && GameObject.FindGameObjectWithTag("DecoyObject").activeInHierarchy)
            {
                target = GameObject.FindGameObjectWithTag("DecoyObject");

            }
            else
            {
                target = player;
            }
    }

    public void TakeDamage(float damageTaken)
    {   if (IsAlive())
        {   health -= damageTaken;
            damageNumbers.text = BuildDamageNumber(damageTaken);
            damageNumberWindow = 0.5f;
            damageNumberAnim.SetTrigger("TakingDamage");
            enemyAnim.SetTrigger("TakeDamage");
            SoundManager.Instance.PlayEnemySFX(gameObject, hitSound, 1);
           
        }
    }


    protected string BuildDamageNumber(float damage)
    {
        string source = damageNumbers.text;
        int count = source.Split('\n').Length;
        if (count > 4 ||  damageNumberAnim.GetCurrentAnimatorStateInfo(0).IsName("NotTakingDamage"))
        {
            damageNumbers.text = "";
            count = 0;
            damageNumbers.gameObject.transform.parent.transform.parent = gameObject.transform;
            damageNumbers.gameObject.transform.parent.position = gameObject.transform.position;
            damageNumbers.gameObject.transform.parent.localPosition = new Vector2(0, 1.42f);
        }
        StringBuilder builder = new StringBuilder(damageNumbers.text);
        if (count > 0)
        {
            
            builder.Insert(0, ((int)damage).ToString());
            builder.Insert(0, " ", count);
            builder.Insert(0, "\n");
        }
        else
        {
            builder.Append("\n");
            builder.Append(((int)damage).ToString());
        }
        if (transform.childCount > 0)
        {
            damageNumbers.gameObject.transform.parent.transform.parent = null;
        }
        return builder.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                StartCoroutine(RemoveForce(rb, 1f));
            }
        }
    }


    private System.Collections.IEnumerator RemoveForce(Rigidbody2D rb, float delay)
    {
      
        yield return new WaitForSeconds(delay);
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
      
    }


    public float GetHealth() 
    {
        return health; 
    }

    protected bool IsAlive()
    {
        if (health <= 0)
        {
            enemyAnim.SetTrigger("Dead");
            Invoke("DestroyGameObject", 0.8f);
            Invoke("RemoveText", 0.8f);
            return false;
        }
        return true;
    }


    //This should instead reattach the gameobject to the enemy if we are to reuse the gameobject instead of destroying them.
    private void RemoveText()
    {
        Destroy(damageNumbers.transform.parent.gameObject);
    }

    
    protected void Drops()
    {
        foreach (DropItem drop in drops)
        {
            if (Drop(drop) && !multiDrop)
            {
                break;  // Stop further drops if multiDrop is not allowd
            }
        }
    }

    protected bool Drop(DropItem drop)
    {
        if (UnityEngine.Random.Range(0f, 100f) <= drop.dropChance)
        {
            GameObject pooledObject = PoolController.Instance.GetPooledObject(drop.item.name);
            if (pooledObject != null)
            {   
                if(drop.item.name == "XPDrop"){
                    pooledObject.GetComponent<XPDrop>().Initialize(xpValue);
                }
                pooledObject.transform.position = transform.position;
                return true;

            }else{
                GameObject dropInstance = Instantiate(drop.item, transform.position, Quaternion.identity);
                dropInstance.SetActive(true);
                return true;
            }
        }
        return false;
    }
    public void UpdateSpeed(){
        thisMovementSpeed = movementSpeed * baseMovementSpeed;
       
    }

    protected virtual void DestroyGameObject()
    {   
    
        Drops();
        
        
        ResultManager.Instance.enemiesDefeated += 1;
        Destroy(gameObject);
    }

    protected int GetRandomInt (int a, int b)
    {
        return UnityEngine.Random.Range(a, b);
    }

    
}
