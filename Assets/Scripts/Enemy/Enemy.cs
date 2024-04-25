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
    public int dropChance_0_To_100;
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



    public static float movementSpeed;
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
    }


    void FixedUpdate()
    {
        damageNumberWindow -= Time.deltaTime;
        if (!isSlow)
        {
            UppdateSpeed();
        }

        if (IsAlive()) 
        {
            if(GameObject.FindGameObjectWithTag("Decoy") != null && GameObject.FindGameObjectWithTag("Decoy").activeInHierarchy)
            {
                target = GameObject.FindGameObjectWithTag("Decoy");

            }
            else
            {
                target = GameObject.FindGameObjectWithTag("Player");
            }

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


    protected bool Drop(GameObject drope, int dropeChans)
    {   int random = UnityEngine.Random.Range(0, 101);
        if(random <= dropeChans)
        {   
            if (drope) {
                GameObject enemyDrop = Instantiate(drope, transform.position, Quaternion.identity);
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
            if (Drop(drop.item, drop.dropChance_0_To_100 )) {
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
