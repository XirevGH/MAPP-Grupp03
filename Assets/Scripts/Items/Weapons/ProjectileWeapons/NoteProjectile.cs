using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteProjectile : Projectile { 
    
    private Vector3 direction;

    [SerializeField] private Sprite sprite1, sprite2;
    private float lifetime = 5f;

   public void Initialize(float damage, float speed, int penetration, Vector3 direction)
    {
        base.damage = damage;
        base.speed = speed;
        base.penetration = penetration;  
        this.direction = direction;
        
        GetComponent<SpriteRenderer>().sprite = randomSprite();
        
    }

    private Sprite randomSprite(){
        if(Random.Range(0, 2) == 1){
            return sprite1;
        }else{
            return sprite2;
        }
    }
    

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DealDamage(other, damage);
        DestroyWhenMaxPenetration();
    }
    
}