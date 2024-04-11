using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ElectricGuitar : Weapon
{
    [SerializeField] private GameObject bolt;
    [SerializeField] private int bounces;

    private LinkedList<GameObject> enemies = new LinkedList<GameObject>();
    private GameObject closestEnemy;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            enemies.AddLast(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        enemies.Remove(other.gameObject);
    }

    public override void Attack()
    {
        float shortestDistance = Mathf.Infinity;
        foreach(GameObject enemy in enemies)
        {
            if(enemy != null)
            {
                if((enemy.transform.position - transform.position).sqrMagnitude < shortestDistance)
                {
                    shortestDistance = (enemy.transform.position - transform.position).sqrMagnitude;
                    closestEnemy = enemy;
                }
            }
        }
        Debug.Log("Attacked");
        if(closestEnemy != null)
        {
            GameObject clone = Instantiate(bolt);
            clone.SetActive(true);
            StartCooldown();
        }
    }

    public GameObject getEnemy()
    {
        return closestEnemy;
    }

    public int getStartingBounces()
    {
        return bounces;
    }
}
