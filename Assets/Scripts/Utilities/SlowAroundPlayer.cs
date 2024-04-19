using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class SlowAroundPlayer : MonoBehaviour
{
    [SerializeField] private float slowSpeed;
    private HashSet<GameObject> enemies = new HashSet<GameObject>();
    private Dictionary<GameObject, float> enemiesMoveSpeed = new Dictionary<GameObject, float>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().thisMovementSpeed = slowSpeed;
           
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
            enemiesMoveSpeed.Add(other.gameObject, other.gameObject.GetComponent<Enemy>().thisMovementSpeed);
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().thisMovementSpeed = enemiesMoveSpeed[other.gameObject];
            enemies.Remove(other.gameObject);
            enemiesMoveSpeed.Remove(other.gameObject);

        }
    }
}
