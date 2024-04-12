using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaxophoneWeapon : Weapon
{
    public Transform shootingPoint;
    public GameObject notePrefab; 

    public float speed;
    
    public int penetration;  

    private List<GameObject> enemies = new List<GameObject>();

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !enemies.Contains(other.gameObject))
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
        }
    }

    public override void Attack()
    {
        GameObject closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            ShootNoteAtEnemy(closestEnemy);
            StartCooldown();
        }
    }

    
    public new void DealDamage(Collider2D other)
    {
       //do nothing
    }
    

    private GameObject FindClosestEnemy()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = (enemy.transform.position - shootingPoint.position).sqrMagnitude;
            if (enemy != null && distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }

    private void ShootNoteAtEnemy(GameObject enemy)
    {
        if (notePrefab)
        {
            GameObject note = Instantiate(notePrefab, shootingPoint.position, Quaternion.identity);
            note.SetActive(true);
            NoteProjectile noteProjectile = note.GetComponent<NoteProjectile>();
            if (noteProjectile != null)
            {
                Vector3 direction = (enemy.transform.position - shootingPoint.position).normalized;
                noteProjectile.Initialize( damage, speed, penetration, direction ); 
            }
        }
    }
    public void UpgradePirceAndSpeed(float speedAdd, int penetrationAdd) 
    {
        speed += speedAdd;
        penetration += penetrationAdd;
    }
    
}
