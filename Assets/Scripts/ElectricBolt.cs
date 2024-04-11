using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
        guitar = guitar.gameObject;
        enemy = guitar.GetComponent<ElectricGuitar>().getEnemy();
        transform.position = guitar.transform.position;
        direction = enemy.transform.position - guitar.transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    //private IEnumerator Shoot()
}
