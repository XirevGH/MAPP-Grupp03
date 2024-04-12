using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteProjectile : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 5f;
    public int penetration = 1; // Number of enemies this note can penetrate

    private Vector3 direction;

    public void Initialize(Vector3 direction, int startPenetration)
    {
        this.direction = direction;
        this.penetration = startPenetration;
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && penetration > 0)
        {
            other.GetComponent<Enemy>().TakeDamage(damage); // Assume Enemy has a TakeDamage method
            penetration--;
            if (penetration <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}