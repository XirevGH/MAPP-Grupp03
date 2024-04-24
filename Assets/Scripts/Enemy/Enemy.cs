using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using System;
using System.Linq;
<<<<<<< Updated upstream
using Unity.VisualScripting;
=======
using static UnityEngine.GraphicsBuffer;
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream
    //om den kan droppa ferla saker än en. Börja med först droppet i listan
    public bool multiDrop;

    public GameObject player;

    protected float damageNumberWindow = 3f;

    public SpriteRenderer sprite;
=======
    private SpriteRenderer sprite;
    private Vector2 target;
>>>>>>> Stashed changes

    public static float movementSpeed;
    public static float healthProsenIncreas = 1f;
    public float health;

    public float thisMovementSpeed;
    public bool isSlow;

    public TMP_Text damageNumbers;
    public Animator damageNumberAnim;
    public Animator enemyAnim;

    public void Start()
    {   
        health *= healthProsenIncreas;
        player = GameObject.FindGameObjectWithTag("Player");
        sprite = GetComponent<SpriteRenderer>();
        thisMovementSpeed = movementSpeed;
        isSlow = false;
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

<<<<<<< Updated upstream
    protected virtual void FixedUpdate(){}
=======
    void FixedUpdate()
    {
        damageNumberWindow -= Time.deltaTime;
        if (!isSlow)
        {
            thisMovementSpeed = movementSpeed;
        }

        if (IsAlive()) 
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 0.5)
            {
                player.GetComponent<Player>().TakeDamage(1);
            }
            if (transform.position.x < target.x)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }

            enemyAnim.SetTrigger("Walking");
            if (GameObject.FindGameObjectWithTag("Decoy") != null && GameObject.FindGameObjectWithTag("Decoy").activeInHierarchy)
            {
                target = GameObject.FindGameObjectWithTag("Decoy").transform.position;
             
            }
            else 
            {
                target = GameObject.FindGameObjectWithTag("Player").transform.position;
            }

            transform.position = Vector3.MoveTowards(transform.position, target, thisMovementSpeed / 200);
        }
           
        
      
    }

>>>>>>> Stashed changes
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
