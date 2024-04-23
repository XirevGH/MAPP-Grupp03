using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class XPDrop : MonoBehaviour
{
    [SerializeField] private int XP;

    public float speed = 20f;

    private bool move = false;

    private Transform target;
    private GameController gameController;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gameController.AddXpObject(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().AddXP(XP);
            gameController.RemoveXpObject(this);
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (move && target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public void MoveToPlayer(Transform playerTransform)
    {
        target = playerTransform;
        move = true;
    }
}