using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ElectricBolt : MonoBehaviour
{
    [SerializeField] private Sprite bolt1, bolt2;

    public GameObject startingUnit;
    private GameObject targetUnit;
    private int bouncesLeft;

    private float length;
    private float angle;
    private Vector3 direction;
    private float spriteChangeCooldown = 0.3f;
    private bool spriteChangeReady;
    private float lifetime;

    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        targetUnit = startingUnit.GetComponent<ElectricGuitar>().getEnemy();
        bouncesLeft = startingUnit.GetComponent<ElectricGuitar>().getStartingBounces();
        spriteChangeReady = true;
        lifetime = startingUnit.GetComponent<ElectricGuitar>().getLifetime();
    }

    private void Update()
    {
        length = Vector3.Distance(startingUnit.transform.position, targetUnit.transform.position);
        startingUnit = startingUnit.gameObject;
        targetUnit = startingUnit.GetComponent<ElectricGuitar>().getEnemy();
        direction = targetUnit.transform.position - startingUnit.transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = startingUnit.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.localScale = new Vector3(length / 10.24f, transform.localScale.y, transform.localScale.z);
    }

    private void FixedUpdate()
    {
        lifetime -= Time.deltaTime;
        if(lifetime < 0)
        {
            Destroy(gameObject);
        }

        if(spriteChangeReady)
        {
            ChangeSprite();
            spriteChangeReady = false;
            Invoke("SetSpriteReady", spriteChangeCooldown);
        }
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

    //private IEnumerator Shoot()
}
