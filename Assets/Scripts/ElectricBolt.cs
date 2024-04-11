using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ElectricBolt : MonoBehaviour
{
    [SerializeField] private Sprite bolt1, bolt2;
    [SerializeField] private GameObject guitar;

    private GameObject enemy;
    private int bouncesLeft;

    private float length;
    private float angle;
    private Vector3 direction;

    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        enemy = guitar.GetComponent<ElectricGuitar>().getEnemy();
        bouncesLeft = guitar.GetComponent<ElectricGuitar>().getStartingBounces();
    }

    private void Update()
    {
        length = Vector3.Distance(guitar.transform.position, enemy.transform.position);
        guitar = guitar.gameObject;
        enemy = guitar.GetComponent<ElectricGuitar>().getEnemy();
        transform.position = (guitar.transform.position + enemy.transform.position) / 2f;
        direction = enemy.transform.position - guitar.transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.localScale = new Vector3(length / 10.24f, transform.localScale.y, transform.localScale.z);
    }

    //private IEnumerator Shoot()
}
