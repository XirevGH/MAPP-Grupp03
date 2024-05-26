using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Saxophone : ProjectileWeapon
{
    public Transform shootingPoint;
    public GameObject notePrefab; 
    public float speed;
    private List<GameObject> enemies = new List<GameObject>();


    private void Start()
    {
        UnityAction action = new UnityAction(Attack);
        TriggerController.Instance.SetTrigger(3, action);
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

    public override void Attack()
    {
        if (gameObject.activeSelf)
        {
            List<GameObject> closestEnemy = FindClosestEnemy(base.amountOfProjectiles);
            if (closestEnemy.Count != 0)
            {
                SoundManager.Instance.PlaySFX(attackSound, 0.5f);
                foreach (GameObject target in closestEnemy)
                {

                    ShootNoteAtEnemy(target);
                }
                //StartCooldown();
            }
        }
    }

    private List<GameObject> FindClosestEnemy(int number)
    {
        if (enemies == null || enemies.Count == 0 || number <= 0)
            return new List<GameObject>();  

        List<KeyValuePair<float, GameObject>> distances = new List<KeyValuePair<float, GameObject>>();

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                float distance = (enemy.transform.position - shootingPoint.position).sqrMagnitude;
                distances.Add(new KeyValuePair<float, GameObject>(distance, enemy));
            }
        }

        
        distances.Sort((a, b) => a.Key.CompareTo(b.Key));

        
        List<GameObject> enemiesToAttack = new List<GameObject>();
        for (int i = 0; i < Math.Min(number, distances.Count); i++)
        {
            enemiesToAttack.Add(distances[i].Value);
        }

    return enemiesToAttack;
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
}
