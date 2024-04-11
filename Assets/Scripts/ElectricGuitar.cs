using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ElectricGuitar : Weapon
{
    private Sprite bolt1;
    private Sprite bolt2;

    private LinkedList<GameObject> enemies = new LinkedList<GameObject>();

    private void Update()
    {
        DrawClosestLine();
        base.Update();
    }

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

    private void DrawClosestLine()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;
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
        if(closestEnemy != null)
        {
            Debug.DrawLine(transform.position, closestEnemy.transform.position);
        }
        //ShootBolt(closestEnemy);
    }

    private void ShootBolt(GameObject enemy)
    {

    }

    public override void Attack()
    {
        StartCooldown();
        Debug.Log("Attacked");
    }
}
