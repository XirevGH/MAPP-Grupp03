using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(this.GetComponent<Transform>().position, player.GetComponent<Transform>().position, 0.1f);
    }
}
