using UnityEngine;

public class ElectricBolt : MonoBehaviour
{
    [SerializeField] private Sprite bolt1, bolt2;
    [SerializeField] private float lifetime;
   
    private GameObject player;
    private GameObject targetUnit;
    private float damage;

    private float length;
    private float angle;
    private Vector3 direction;
    private float spriteChangeCooldown = 0.1f;
    private bool spriteChangeReady;

    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        damage = player.GetComponent<ElectricGuitar>().GetDamage();
        spriteChangeReady = true;
        DealDamage();
    }

    private void Update()
    {
        if(targetUnit == null)
        {
            Destroy(gameObject);
            return;
        }
        if (player != null) { 
            length = Vector3.Distance(player.transform.position, targetUnit.transform.position);
            direction = targetUnit.transform.position - player.transform.position;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.position = player.transform.position;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.localScale = new Vector3(length / 10.24f, transform.localScale.y, transform.localScale.z);
        }
    }

    private void FixedUpdate()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }

        if (spriteChangeReady)
        {
            ChangeSprite();
            spriteChangeReady = false;
            Invoke("SetSpriteReady", spriteChangeCooldown);
        }
    }

    public void SetPlayerObject(GameObject playerObject)
    {
        player = playerObject;
    }

    public void SetTargetUnit(GameObject target)
    {
        targetUnit = target;
    }

    private void DealDamage()
    {
        targetUnit.GetComponent<Enemy>().TakeDamage(damage);
    }

    private void ChangeSprite()
    {
        if(rend.sprite == bolt1)
        {
            rend.sprite = bolt2;
        }
        else
        {
            rend.sprite = bolt1;
        }

    }

    private void SetSpriteReady()
    {
        spriteChangeReady = true;
    }
}
