using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaxophoneWeapon : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject notePrefab; // Prefab for the note projectile
    public float shootingInterval = 2.0f; // Time between shots

    private float nextShotTime = 0f;
    private List<GameObject> enemies = new List<GameObject>();

    private void Update()
    {
        if (Time.time >= nextShotTime)
        {
            Attack();
            nextShotTime = Time.time + shootingInterval;
        }
    }

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

    private void Attack()
    {
        GameObject closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            ShootNoteAtEnemy(closestEnemy);
        }
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
                noteProjectile.Initialize(direction, 1); // Assuming 1 is the initial penetration value
            }
        }
    }
}
