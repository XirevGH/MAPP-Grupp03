using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using System;
using System.Linq;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;


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
    public GameObject item;

    //chansen i int "%" hur stor changen är att den droppas
    public float dropChance;
}
public class Enemy : MonoBehaviour
{

    [SerializeField] private Sprite enemySprite;
    [SerializeField] private List<DropItem> drops = new List<DropItem>();
    //om den kan droppa ferla saker än en. Börja med först droppet i listan
    public bool multiDrop;
    public GameObject player, target;
    protected float damageNumberWindow = 3f;
    public SpriteRenderer sprite;
    public static float movementSpeed;  // är % * till thisEnmey
    public static float healthProsenIncreas = 1f;
    public float health;
     
    public float thisMovementSpeed; 

    public float baseMovementSpeed;
    public bool isSlow;

    protected float startingHealth;

    public TMP_Text damageNumbers;
    public Animator damageNumberAnim;
    public Animator enemyAnim;


    public void Start()
    {   
        health *= healthProsenIncreas;
        startingHealth = health;
        player = GameObject.FindGameObjectWithTag("Player");
        sprite = GetComponent<SpriteRenderer>();
        UppdateSpeed();
        isSlow = false;
        target = player;
        StaticUpdateManager.RegisterUpdate(CustomSlowUpdate);
    }

    protected virtual void CustomSlowUpdate() //Slow uppdate 0.4s
    {
         if (!isSlow)
        {
            UppdateSpeed();
            
        }
        
    }

    void FixedUpdate()
    {
        SelectTarget();
        damageNumberWindow -= Time.deltaTime;
        

        if (IsAlive()) 
        {
           

            if (Vector3.Distance(target.transform.position, transform.position) < 0.5)
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
         if(GameObject.FindGameObjectWithTag("Decoy") != null && GameObject.FindGameObjectWithTag("Decoy").activeInHierarchy)
            {
                target = GameObject.FindGameObjectWithTag("Decoy");

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
            if (damageNumberWindow <= 0)
            {
                damageNumberWindow = 3f;
                damageNumberAnim.SetTrigger("TakingDamage");
            }
            enemyAnim.SetTrigger("TakeDamage");
        }
    }

    protected string BuildDamageNumber(float damage)
    {
        string source = damageNumbers.text;
        int count = source.Split('\n').Length;
        if (count > 4 || damageNumberWindow <= 0)
        {
            damageNumbers.text = "";
            count = 0;
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
            builder.Append("\n");
            builder.Append(damage.ToString());
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
            rb.angularVelocity = 0f;
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
            return false;
        }
        return true;
    }


    protected bool Drop(GameObject drop, float dropChance)
    {   float random = (float)Math.Round(UnityEngine.Random.Range(0f, 100f), 4);
        if (random <= dropChance)
        {   
            if (drop) {
                GameObject enemyDrop = Instantiate(drop, transform.position, Quaternion.identity);
                enemyDrop.SetActive(true);
                return true;
            } else {
            Debug.LogWarning("Drop item is null.");
            }
        }
        return false;
        
    }
    protected void Drops() {
        foreach (DropItem drop in drops) { 
            if (Drop(drop.item, drop.dropChance )) {
                if (!multiDrop) {
                    break;
                }
            }
        }
    }

    public void UppdateSpeed(){
        thisMovementSpeed = movementSpeed * baseMovementSpeed;
    }


    

    protected virtual void DestroyGameObject()
    {
        Drops();
        MainManager.Instance.enemiesDefeated += 1;
        Destroy(gameObject);
    }

    protected int GetRandomInt (int a, int b)
    {
        return UnityEngine.Random.Range(a, b);
    }

    
}
