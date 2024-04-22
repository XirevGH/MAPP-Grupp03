using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class XPDrop : MonoBehaviour
{
    [SerializeField] private int XP;

    private Transform target;
    private bool move = false;
    public float speed = 20f;

    private GameObject gameControllerObject;
    private GameController gameController;

    private void Awake()
    {
        gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
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