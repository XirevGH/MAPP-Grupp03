using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class XPDrop : MonoBehaviour
{
    [SerializeField] private int XP;

    private GameObject player;
    private Player playerScript;

    static private GameController gameController;
    public float speed = 20f;

    private bool move = false;

    private Transform target;
    

    private void Awake()
    {   playerScript =  Player.Instance;
        player = playerScript.GameObject();
        if(gameController == null){
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
        
        gameController.AddXpObject(this);
    }

    

    void FixedUpdate()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < 2 && move != true){
            MoveToPlayer(player.transform);
             
        }
        if (move && player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if(Vector3.Distance(player.transform.position, transform.position) < 0.3){
                playerScript.AddXP(XP);
                gameController.RemoveXpObject(this);
                Destroy(gameObject);
            }
        } 
       
    }

    public void MoveToPlayer(Transform playerTransform)
    {
        target = playerTransform;
        move = true;
    }
}