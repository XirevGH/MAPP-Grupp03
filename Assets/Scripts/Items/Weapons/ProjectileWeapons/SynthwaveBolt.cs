using UnityEngine;

public class SynthwaveBolt : Projectile
{
    private void Start()
    {
        SquishBolt();
    }

    void SquishBolt()
    {
        transform.localScale = new Vector2(0, transform.localScale.y);
    }

    void MoveBolt()
    {
        if (transform.localScale.x < 1)
        {
            transform.localScale = new Vector2(transform.localScale.x + 0.1f, transform.localScale.y);
        }
        else
        {
            transform.localPosition = new Vector2(transform.localPosition.x + 0.8f, transform.localPosition.y);
        }
    }

    private void FixedUpdate()
    {
        MoveBolt();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DealDamage(other);
        DestroyWhenMaxPenetration();
    }
}
