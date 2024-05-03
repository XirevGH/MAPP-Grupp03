using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiscoBall : Projectile
{ 
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float height, distance, torque;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        distance = Random.Range(-distance, distance);
       
        rb.AddForce( new Vector2 (distance, height - Mathf.Abs(distance)));
        rb.AddTorque(torque * Mathf.Sign(distance));
       
        spriteRenderer.color = Color.HSVToRGB(Random.Range(0f, 1f), 0.7f, 1 );
       

    }

    public void Blink() 
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.HSVToRGB(Random.Range(0f, 1f), 0.7f, 1); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DealDamage(other);
        DestroyWhenMaxPenetration();
    }

    public override void DestroyWhenMaxPenetration()
    {
        DiscoBallController.Instance.activeDiscoBalls.Remove(gameObject);
        base.DestroyWhenMaxPenetration();

    }

  
}